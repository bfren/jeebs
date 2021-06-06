// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.Linq;
using static F.DataF.QueryF;

namespace Jeebs.Data
{
	public abstract partial class DbClient : IDbClient
	{
		/// <summary>
		/// Return a query to retrieve a list of entities that match all the specified parameters
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="columns">List of columns to select</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		protected abstract (string query, IQueryParameters param) GetQuery(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		);

		/// <inheritdoc/>
		public Option<(string query, IQueryParameters param)> GetQuery<TEntity, TModel>(
			(Expression<Func<TEntity, object>>, SearchOperator, object)[] predicates
		)
			where TEntity : IEntity =>
			(
				from map in Mapper.GetTableMapFor<TEntity>()
				from sel in Extract<TModel>.From(map.Table)
				from whr in ConvertPredicatesToColumns(map.Columns, predicates)
				select (map, sel, whr)
			)
			.Map(
				x => GetQuery(x.map.Name, x.sel, x.whr),
				e => new Msg.ErrorGettingGeneralRetrieveQueryExceptionMsg(e)
			);

		/// <inheritdoc/>
		public (string query, IQueryParameters param) GetCountQuery(IQueryParts parts) =>
			GetQuery(new QueryParts(parts) with { Select = new() });

		/// <inheritdoc/>
		public abstract (string query, IQueryParameters param) GetQuery(IQueryParts parts);

		#region Testing

		internal (string query, IQueryParameters param) GetQueryTest(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		) =>
			GetQuery(table, columns, predicates);

		#endregion
	}
}
