// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Collections;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Query;

/// <summary>
/// Build an <see cref="QueryParts"/> object
/// This must be in Jeebs.Data project as it requires access to the <see cref="QueryParts"/> definition
/// </summary>
/// <typeparam name="TId">Entity ID type.</typeparam>
public interface IQueryPartsBuilder<TId>
	where TId : class, IUnion, new()
{
	/// <summary>
	/// The primary table for this query.
	/// </summary>
	ITable Table { get; }

	/// <summary>
	/// ID Column details.
	/// </summary>
	IColumn IdColumn { get; }

	/// <summary>
	/// Retrieve columns matching the specified model.
	/// </summary>
	/// <typeparam name="TModel">Return Model type.</typeparam>
	Result<IColumnList> GetColumns<TModel>();

	/// <summary>
	/// Create a new QueryParts object, adding <paramref name="maximum"/> and <paramref name="skip"/> values.
	/// </summary>
	/// <typeparam name="TModel">Return Model type.</typeparam>
	/// <param name="maximum">Maximum number of results to select.</param>
	/// <param name="skip">Number of results to skip.</param>
	Result<QueryParts> Create<TModel>(ulong? maximum, ulong skip);

	/// <summary>
	/// Add Join.
	/// </summary>
	/// <typeparam name="TFrom">From Table type.</typeparam>
	/// <typeparam name="TTo">To Table type.</typeparam>
	/// <param name="parts">QueryParts.</param>
	/// <param name="fromTable">From table - should already be added to the query.</param>
	/// <param name="fromSelector">From column.</param>
	/// <param name="toTable">To table - should be a new table not already added to the query.</param>
	/// <param name="toSelector">To column.</param>
	Result<QueryParts> AddInnerJoin<TFrom, TTo>(QueryParts parts,
		TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
		where TFrom : ITable
		where TTo : ITable;

	/// <inheritdoc cref="AddInnerJoin"/>
	Result<QueryParts> AddLeftJoin<TFrom, TTo>(QueryParts parts,
		TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
		where TFrom : ITable
		where TTo : ITable;

	/// <inheritdoc cref="AddInnerJoin"/>
	Result<QueryParts> AddRightJoin<TFrom, TTo>(QueryParts parts,
		TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
		where TFrom : ITable
		where TTo : ITable;

	/// <summary>
	/// Add Id / Ids - Id takes precedence over Ids.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="id">Single ID.</param>
	/// <param name="ids">List of IDs.</param>
	Result<QueryParts> AddWhereId(QueryParts parts,
		TId? id, IImmutableList<TId> ids);

	/// <summary>
	/// Add Sort - SortRandom takes precendence over Sort.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="sortRandom">If true, will sort results randomly.</param>
	/// <param name="sort">Sort columns.</param>
	Result<QueryParts> AddSort(QueryParts parts,
		bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort);

	/// <summary>
	/// Add a Where predicate using Linq Expressions.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="parts">QueryParts.</param>
	/// <param name="table">Table object.</param>
	/// <param name="column">Column selector.</param>
	/// <param name="cmp">Compare operator.</param>
	/// <param name="value">Search value.</param>
	Result<QueryParts> AddWhere<TTable>(QueryParts parts,
		TTable table, Expression<Func<TTable, string>> column, Compare cmp, object value)
		where TTable : ITable;

	/// <summary>
	/// Add a custom Where predicate.
	/// </summary>
	/// <param name="parts">QueryParts.</param>
	/// <param name="clause">Clause text.</param>
	/// <param name="parameters">Clause parameters.</param>
	Result<QueryParts> AddWhereCustom(QueryParts parts,
		string clause, object parameters);
}
