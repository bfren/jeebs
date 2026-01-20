// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map;

/// <summary>
/// Maps entities to tables.
/// </summary>
public interface IEntityMapper
{
	/// <summary>
	/// Map the specified <typeparamref name="TEntity"/> to the specified <paramref name="table"/>.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table to map.</param>
	ITableMap Map<TEntity, TTable>(TTable table)
		where TEntity : IWithId
		where TTable : ITable;

	/// <summary>
	/// Get table map for <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	Result<ITableMap> GetTableMapFor<TEntity>()
		where TEntity : IWithId;
}
