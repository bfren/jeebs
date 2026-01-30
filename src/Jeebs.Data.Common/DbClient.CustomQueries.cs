// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// Return a query to retrieve a list of entities that match all the specified parameters.
	/// </summary>
	/// <param name="table">Table name.</param>
	/// <param name="columns">List of columns to select.</param>
	/// <param name="predicates">Predicates (matched using AND).</param>
	protected abstract Result<(string query, IQueryParametersDictionary param)> GetQuery(
		IDbName table,
		IColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, object value)> predicates
	);

	/// <inheritdoc/>
	public Result<(string query, IQueryParametersDictionary param)> GetQuery<TEntity, TModel>(
		(string, Compare, dynamic)[] predicates
	)
		where TEntity : IWithId =>
		from map in Entities.GetTableMapFor<TEntity>()
		from sel in Extract<TModel>.From(map.Table)
		from whr in DataF.ConvertPredicatesToColumns(map.Columns, predicates)
		from qry in GetQuery(map.Name, sel, whr)
		select qry;

	/// <inheritdoc/>
	public Result<(string query, IQueryParametersDictionary param)> GetCountQuery(IQueryParts parts) =>
		GetQuery(new QueryParts(parts) with { SelectCount = true });

	/// <inheritdoc/>
	public abstract Result<(string query, IQueryParametersDictionary param)> GetQuery(IQueryParts parts);

	#region Testing

	internal Result<(string query, IQueryParametersDictionary param)> GetQueryTest(
		IDbName table,
		IColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, object value)> predicates
	) =>
		GetQuery(table, columns, predicates);

	#endregion Testing
}
