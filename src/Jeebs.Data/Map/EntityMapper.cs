// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Concurrent;
using Jeebs.Data.Attributes;
using Jeebs.Data.Exceptions;
using Jeebs.Data.Map.Functions;

namespace Jeebs.Data.Map;

/// <inheritdoc cref="IEntityMapper"/>
internal sealed class EntityMapper : IEntityMapper, IDisposable
{
	#region Static

	/// <summary>
	/// Default (global) instance.
	/// </summary>
	internal static IEntityMapper Instance =>
		LazyInstance.Value;

	/// <summary>
	/// Lazily create a <see cref="EntityMapper"/>.
	/// </summary>
	private static Lazy<IEntityMapper> LazyInstance { get; } = new(() => new EntityMapper(), true);

	#endregion Static

	/// <summary>
	/// Mapped entities.
	/// </summary>
	private readonly ConcurrentDictionary<Type, TableMap> mappedEntities = new();

	/// <summary>
	/// Only allow internal creation.
	/// </summary>
	internal EntityMapper() { }

	/// <inheritdoc/>
	public ITableMap Map<TEntity, TTable>(TTable table)
		where TEntity : IWithId
		where TTable : ITable =>
		mappedEntities.GetOrAdd(typeof(TEntity), _ =>
		{
			// Validate table
			var (valid, errors) = MapF.ValidateTable<TTable, TEntity>();
			if (!valid)
			{
				throw new InvalidTableMapException(errors);
			}

			// Create table map
			var colResult = MapF.GetColumns<TTable, TEntity>(table);
			var mapResult = from col in colResult
							from id in MapF.GetIdColumn<TTable>(col)
							select new TableMap(table, col, id);

			// Get Version property
			if (typeof(TEntity).Implements<IWithVersion>())
			{
				var mapWithVersion = from col in colResult
									 from map in mapResult
									 from version in MapF.GetColumnWithAttribute<TTable, VersionAttribute>(col)
									 select map with { VersionColumn = version };
				return mapWithVersion.Unwrap(f => throw new InvalidTableMapException(f.ToString()));
			}

			// Return map without Version property
			return mapResult.Unwrap(f => throw new InvalidTableMapException(f.ToString()));
		});

	/// <inheritdoc/>
	public Result<ITableMap> GetTableMapFor<TEntity>()
		where TEntity : IWithId
	{
		if (mappedEntities.TryGetValue(typeof(TEntity), out var map))
		{
			return map;
		}

		return R.Fail(nameof(EntityMapper), nameof(GetTableMapFor),
			"Trying to get table map for an umapped entity.", typeof(TEntity).Name
		);
	}

	#region Dispose

	/// <summary>
	/// Set to true if the object has been disposed.
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
}
