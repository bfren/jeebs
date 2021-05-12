// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Build an <see cref="IQueryParts"/> object
	/// </summary>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public interface IQueryPartsBuilder<TId>
		where TId : StrongId
	{
		/// <summary>
		/// Create a new QueryParts object, adding <paramref name="select"/> columns and
		/// <see cref="Maximum"/> and <see cref="Skip"/> values
		/// </summary>
		/// <param name="table">Primary table</param>
		/// <param name="select">Columns to select</param>
		/// <param name="maximum">Maximum number of results to select</param>
		/// <param name="skip">Number of results to skip</param>
		Option<QueryParts> Create(ITable table,
			IColumnList select, long? maximum, long skip);

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		Option<QueryParts> AddInnerJoin<TFrom, TTo>(QueryParts parts,
			TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
			where TFrom : ITable
			where TTo : ITable;

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		Option<QueryParts> AddLeftJoin<TFrom, TTo>(QueryParts parts,
			TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
			where TFrom : ITable
			where TTo : ITable;

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		Option<QueryParts> AddRightJoin<TFrom, TTo>(QueryParts parts,
			TFrom fromTable, Expression<Func<TFrom, string>> fromSelector, TTo toTable, Expression<Func<TTo, string>> toSelector)
			where TFrom : ITable
			where TTo : ITable;

		/// <summary>
		/// Add Id / Ids - Id takes precedence over Ids
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="idColumn">ID Column</param>
		/// <param name="id">Single ID</param>
		/// <param name="ids">List of IDs</param>
		Option<QueryParts> AddWhereId(QueryParts parts,
			IColumn idColumn, TId? id, IImmutableList<TId> ids);

		/// <summary>
		/// Add Sort - SortRandom takes precendence over Sort
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="sortRandom">If true, will sort results randomly</param>
		/// <param name="sort">Sort columns</param>
		Option<QueryParts> AddSort(QueryParts parts,
			bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort);

		/// <summary>
		/// Add a Where predicate using Linq Expressions
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="parts">QueryParts</param>
		/// <param name="table">Table object</param>
		/// <param name="column">Column selector</param>
		/// <param name="cmp">Compare operator</param>
		/// <param name="value">Search value</param>
		Option<QueryParts> AddWhere<TTable>(QueryParts parts,
			TTable table, Expression<Func<TTable, string>> column, Compare cmp, object value)
			where TTable : ITable;

		/// <summary>
		/// Add a custom Where predicate
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="clause">Clause text</param>
		/// <param name="parameters">Clause parameters</param>
		Option<QueryParts> AddWhereCustom(QueryParts parts,
			string clause, object parameters);
	}
}
