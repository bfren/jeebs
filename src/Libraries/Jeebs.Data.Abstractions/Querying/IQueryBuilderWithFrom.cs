// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Once <see cref="IQueryBuilder"/> has defined the table, more query options can be built
	/// </summary>
	public interface IQueryBuilderWithFrom
	{
		/// <summary>
		/// Create a table JOIN
		/// </summary>
		/// <typeparam name="TFrom">Join from table type</typeparam>
		/// <typeparam name="TTo">Join to table type</typeparam>
		/// <param name="join">Join type</param>
		/// <param name="from">Join from this column</param>
		/// <param name="to">Join to this table and column</param>
		IQueryBuilderWithFrom Join<TFrom, TTo>(
			QueryJoin join,
			Expression<Func<TFrom, string>> from,
			Expression<Func<TTo, string>> to
		)
			where TFrom : ITable, new()
			where TTo : ITable, new();

		/// <summary>
		/// Add a WHERE predicate (predicates are added using AND) - you will need to write more complex queries manually
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Table column</param>
		/// <param name="op">Search operator</param>
		/// <param name="value">Search value</param>
		IQueryBuilderWithFrom Where<TTable>(Expression<Func<TTable, string>> column, SearchOperator op, object value)
			where TTable : ITable, new();

		/// <summary>
		/// Sort by column in Ascending order
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Table column</param>
		IQueryBuilderWithFrom SortBy<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new();

		/// <summary>
		/// Sort by column in Descending order
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Table column</param>
		IQueryBuilderWithFrom SortByDescending<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new();

		/// <summary>
		/// Add a limit to the number of results returned by this query
		/// </summary>
		/// <param name="max">The maximum number of results to return</param>
		IQueryBuilderWithFrom Maximum(long max);

		/// <summary>
		/// Skip a number of results before returning
		/// </summary>
		/// <param name="skip">The number of results to skip</param>
		IQueryBuilderWithFrom Skip(long skip);
	}
}
