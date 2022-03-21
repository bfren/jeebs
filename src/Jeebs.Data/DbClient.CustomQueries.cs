// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Data.Query.Functions;
using Jeebs.Id;

namespace Jeebs.Data;

public abstract partial class DbClient : IDbClient
{
	/// <summary>
	/// Return a query to retrieve a list of entities that match all the specified parameters
	/// </summary>
	/// <param name="table">Table name</param>
	/// <param name="columns">List of columns to select</param>
	/// <param name="predicates">Predicates (matched using AND)</param>
	protected abstract (string query, IQueryParametersDictionary param) GetQuery(
		ITableName table,
		IColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, object value)> predicates
	);

	/// <inheritdoc/>
	public Maybe<(string query, IQueryParametersDictionary param)> GetQuery<TEntity, TModel>(
		(Expression<Func<TEntity, object>>, Compare, object)[] predicates
	)
		where TEntity : IWithId =>
		(
			from map in Mapper.GetTableMapFor<TEntity>()
			from sel in Extract<TModel>.From(map.Table)
			from whr in QueryF.ConvertPredicatesToColumns(map.Columns, predicates)
			select (map, sel, whr)
		)
		.Map(
			x => GetQuery(x.map.Name, x.sel, x.whr),
			e => new M.ErrorGettingGeneralRetrieveQueryExceptionMsg(e)
		);

	/// <inheritdoc/>
	public (string query, IQueryParametersDictionary param) GetCountQuery(IQueryParts parts) =>
		GetQuery(new QueryParts(parts) with { SelectCount = true });

	/// <inheritdoc/>
	public abstract (string query, IQueryParametersDictionary param) GetQuery(IQueryParts parts);

	#region Testing

	internal (string query, IQueryParametersDictionary param) GetQueryTest(
		ITableName table,
		ColumnList columns,
		IImmutableList<(IColumn column, Compare cmp, object value)> predicates
	) =>
		GetQuery(table, columns, predicates);

	#endregion Testing
}
