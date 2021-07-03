// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Concurrent;
using Jeebs.Data.Entities;
using Jeebs.Data.Exceptions;
using static F.DataF.MappingF;
using static F.OptionF;

namespace Jeebs.Data.Mapping
{
	/// <inheritdoc cref="IMapper"/>
	internal sealed class Mapper : IMapper, IDisposable
	{
		#region Static

		/// <summary>
		/// Default (global) instance
		/// </summary>
		internal static IMapper Instance =>
			instance.Value;

		/// <summary>
		/// Lazily create a <see cref="Mapper"/>
		/// </summary>
		private static readonly Lazy<IMapper> instance = new(() => new Mapper(), true);

		#endregion

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
				var (valid, errors) = ValidateTable<TEntity>(table);
				if (!valid)
				{
					throw new InvalidTableMapException(errors);
				}

				// Get mapped columns
				var columns = GetMappedColumns<TEntity>(table).Unwrap(
					reason => throw new UnableToGetMappedColumnsException(reason)
				);

				// Get ID property
				var idProperty = GetColumnWithAttribute<TEntity, IdAttribute>(columns).Unwrap(
					reason => throw new UnableToFindIdColumnException(reason)
				) with
				{
					Alias = nameof(IWithId.Id)
				};

				// Create Table Map
				var map = new TableMap(table, columns, idProperty);

				// Get Version property
				if (typeof(TEntity).Implements<IWithVersion>())
				{
					map.VersionColumn = GetColumnWithAttribute<TEntity, VersionAttribute>(columns).Unwrap(
						reason => throw new UnableToFindVersionColumnException(reason)
					);
				}

				// Return map
				return map;
			});

		/// <inheritdoc/>
		public Option<ITableMap> GetTableMapFor<TEntity>()
			where TEntity : IWithId
		{
			if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
			{
				return map;
			}

			return None<ITableMap, Msg.TryingToGetUnmappedEntityMsg<TEntity>>();
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

			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>The entity being requested has not been mapped yet</summary>
			/// <typeparam name="TEntity">Entity type</typeparam>
			public sealed record TryingToGetUnmappedEntityMsg<TEntity> : IMsg { }
		}
	}
}
