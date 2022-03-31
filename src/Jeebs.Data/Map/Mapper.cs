// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Concurrent;
using Jeebs.Data.Attributes;
using Jeebs.Data.Exceptions;
using Jeebs.Data.Map.Functions;
using StrongId;
using Jeebs.Messages;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IMapper"/>
internal sealed class Mapper : IMapper, IDisposable
{
	#region Static

	/// <summary>
	/// Default (global) instance
	/// </summary>
	internal static IMapper Instance =>
		LazyInstance.Value;

	/// <summary>
	/// Lazily create a <see cref="Mapper"/>
	/// </summary>
	private static Lazy<IMapper> LazyInstance { get; } = new(() => new Mapper(), true);

	#endregion Static

	/// <summary>
	/// Mapped entities
	/// </summary>
	private readonly ConcurrentDictionary<Type, TableMap> mappedEntities = new();

	/// <summary>
	/// Only allow internal creation
	/// </summary>
	internal Mapper() { }

	/// <inheritdoc/>
	public ITableMap Map<TEntity>(ITable table)
		where TEntity : IWithId =>
		mappedEntities.GetOrAdd(typeof(TEntity), _ =>
		{
			// Validate table
			var (valid, errors) = MapF.ValidateTable<TEntity>(table);
			if (!valid)
			{
				throw new InvalidTableMapException(errors);
			}

			// Get mapped columns
			var columns = MapF.GetMappedColumns<TEntity>(table).Unwrap(
				reason => throw new UnableToGetMappedColumnsException(reason)
			);

			// Get ID column by attribute (to allow ID to be overridden)
			var idColumn = MapF.GetColumnWithAttribute<TEntity, IdAttribute>(columns).Unwrap(
				_ => MapF.GetIdColumn<TEntity>(columns).Unwrap(
					reason => throw new UnableToFindIdColumnException(reason)
				)
			) with
			{
				ColAlias = nameof(IWithId.Id)
			};

			// Create Table Map
			var map = new TableMap(table, columns, idColumn);

			// Get Version property
			if (typeof(TEntity).Implements<IWithVersion>())
			{
				map.VersionColumn = MapF.GetColumnWithAttribute<TEntity, VersionAttribute>(columns).Unwrap(
					reason => throw new UnableToFindVersionColumnException(reason)
				);
			}

			// Return map
			return map;
		});

	/// <inheritdoc/>
	public Maybe<ITableMap> GetTableMapFor<TEntity>()
		where TEntity : IWithId
	{
		if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
		{
			return map;
		}

		return F.None<ITableMap, M.TryingToGetUnmappedEntityMsg<TEntity>>();
	}

	#region Dispose

	/// <summary>
	/// Set to true if the object has been disposed
	/// </summary>
	private bool disposed;

	/// <summary>
	/// Suppress garbage collection and clear mapped entities
	/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
	/// </summary>
	public void Dispose()
	{
		if (disposed)
		{
			return;
		}

		mappedEntities.Clear();
		disposed = true;
	}

	#endregion Dispose

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The entity being requested has not been mapped yet</summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		public sealed record class TryingToGetUnmappedEntityMsg<TEntity> : Msg;
	}
}
