// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data;

/// <summary>
/// Map an entity to a table in a fluent style
/// </summary>
/// <typeparam name="TEntity">Entity type</typeparam>
public static class Map<TEntity>
	where TEntity : IWithId
{
	/// <inheritdoc/>
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static ITableMap To<TTable>()
#pragma warning restore CA1000 // Do not declare static members on generic types
		where TTable : Table, new() =>
		To<TTable>(EntityMapper.Instance);

	/// <summary>
	/// Map entity to the specified table type
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="mapper">IMapper</param>
	internal static ITableMap To<TTable>(IEntityMapper mapper)
		where TTable : Table, new() =>
		mapper.Map<TEntity, TTable>(new TTable());

	/// <summary>
	/// Map entity to the specified table
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
#pragma warning disable CA1000 // Do not declare static members on generic types
	public static ITableMap To<TTable>(TTable table)
#pragma warning restore CA1000 // Do not declare static members on generic types
		where TTable : Table =>
		To(table, EntityMapper.Instance);

	/// <summary>
	/// Map entity to the specified table
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">The table to map <typeparamref name="TEntity"/> to</param>
	/// <param name="mapper">IMapper</param>
	internal static ITableMap To<TTable>(TTable table, IEntityMapper mapper)
		where TTable : Table =>
		mapper.Map<TEntity, TTable>(table);
}
