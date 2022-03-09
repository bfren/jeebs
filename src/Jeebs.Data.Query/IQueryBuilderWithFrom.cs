// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query;

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
	/// <param name="type">Join type</param>
	/// <param name="fromColumn">Join from this column</param>
	/// <param name="toColumn">Join to this table and column</param>
	IQueryBuilderWithFrom Join<TFrom, TTo>(
		QueryJoin type,
		Expression<Func<TFrom, string>> fromColumn,
		Expression<Func<TTo, string>> toColumn
	)
		where TFrom : ITable, new()
		where TTo : ITable, new();

	/// <summary>
	/// Add a WHERE predicate (predicates are added using AND) - you will need to write more complex queries manually
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="column">Table column</param>
	/// <param name="cmp">Search operator</param>
	/// <param name="value">Search value</param>
	IQueryBuilderWithFrom Where<TTable>(Expression<Func<TTable, string>> column, Compare cmp, object value)
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
	/// <param name="number">The maximum number of results to return</param>
	IQueryBuilderWithFrom Maximum(ulong number);

	/// <summary>
	/// Skip a number of results before returning
	/// </summary>
	/// <param name="number">The number of results to skip</param>
	IQueryBuilderWithFrom Skip(ulong number);
}
