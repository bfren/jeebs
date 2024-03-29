// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Exceptions;
using Jeebs.Data.Query.Functions;
using Jeebs.Messages;
using StrongId;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="QueryPartsBuilder{TId}"/>
public abstract class QueryPartsBuilder
{
	/// <summary>
	/// Get ID column from the specified table
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="table"></param>
	/// <exception cref="UnableToGetIdColumnFromTableException"></exception>
	protected PropertyInfo GetIdColumn<T>(T table) where
		T : ITable =>
		table.GetType().GetProperty(nameof(IWithId.Id)) ?? throw new UnableToGetIdColumnFromTableException();

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Trying to add an empty where clause</summary>
		public sealed record class TryingToAddEmptyClauseToWhereCustomMsg : Msg;

		/// <summary>Unable to add parameters to custom where</summary>
		public sealed record class UnableToAddParametersToWhereCustomMsg : Msg;
	}
}

/// <summary>
/// Builds a <see cref="QueryParts"/> object from various options
/// </summary>
/// <typeparam name="TId">Entity ID type</typeparam>
public abstract class QueryPartsBuilder<TId> : QueryPartsBuilder, IQueryPartsBuilder<TId>
	where TId : class, IStrongId, new()
{
	/// <summary>
	/// IExtract
	/// </summary>
	protected IExtract Extract { get; private init; }

	/// <inheritdoc/>
	public abstract ITable Table { get; }

	/// <inheritdoc/>
	public abstract IColumn IdColumn { get; }

	/// <summary>
	/// Create object with default extractor
	/// </summary>
	protected QueryPartsBuilder() : this(new Extract()) { }

	/// <summary>
	/// Inject extract object
	/// </summary>
	/// <param name="extract">IExtract</param>
	protected QueryPartsBuilder(IExtract extract) =>
		Extract = extract;

	/// <inheritdoc/>
	public QueryParts Create<TModel>(ulong? maximum, ulong skip) =>
		new(Table)
		{
			SelectColumns = GetColumns<TModel>(),
			Maximum = maximum,
			Skip = skip
		};

	/// <inheritdoc/>
	public virtual IColumnList GetColumns<TModel>() =>
		Extract.From<TModel>(Table);

	/// <summary>
	/// Add Join
	/// </summary>
	/// <typeparam name="TFrom">From Table type</typeparam>
	/// <typeparam name="TTo">To Table type</typeparam>
	/// <param name="parts">QueryParts</param>
	/// <param name="fromTable">From table - should already be added to the query</param>
	/// <param name="fromSelector">From column</param>
	/// <param name="toTable">To table - should be a new table not already added to the query</param>
	/// <param name="toSelector">To column</param>
	/// <param name="withJoin">Function to add the Join to the correct list</param>
	protected internal Maybe<QueryParts> AddJoin<TFrom, TTo>(
		QueryParts parts,
		TFrom fromTable,
		Expression<Func<TFrom, string>> fromSelector,
		TTo toTable,
		Expression<Func<TTo, string>> toSelector,
		Func<QueryParts, IColumn, IColumn, QueryParts> withJoin
	)
		where TFrom : ITable
		where TTo : ITable =>
		from colFrom in QueryF.GetColumnFromExpression(fromTable, fromSelector)
		from colTo in QueryF.GetColumnFromExpression(toTable, toSelector)
		select withJoin(parts, colFrom, colTo);

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddInnerJoin<TFrom, TTo>(
		QueryParts parts,
		TFrom fromTable,
		Expression<Func<TFrom, string>> fromSelector,
		TTo toTable,
		Expression<Func<TTo, string>> toSelector
	)
		where TFrom : ITable
		where TTo : ITable =>
		AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
			parts with { InnerJoin = parts.InnerJoin.WithItem((colFrom, colTo)) }
		);

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddLeftJoin<TFrom, TTo>(
		QueryParts parts,
		TFrom fromTable,
		Expression<Func<TFrom, string>> fromSelector,
		TTo toTable,
		Expression<Func<TTo, string>> toSelector
	)
		where TFrom : ITable
		where TTo : ITable =>
		AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
			parts with { LeftJoin = parts.LeftJoin.WithItem((colFrom, colTo)) }
		);

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddRightJoin<TFrom, TTo>(
		QueryParts parts,
		TFrom fromTable,
		Expression<Func<TFrom, string>> fromSelector,
		TTo toTable,
		Expression<Func<TTo, string>> toSelector
	)
		where TFrom : ITable
		where TTo : ITable =>
		AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
			parts with { RightJoin = parts.RightJoin.WithItem((colFrom, colTo)) }
		);

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddWhereId(QueryParts parts, TId? id, IImmutableList<TId> ids)
	{
		// Add Id EQUAL
		if (id is not null)
		{
			return parts with { Where = parts.Where.WithItem((IdColumn, Compare.Equal, id.Value)) };
		}

		// Add Id IN
		else if (ids.Count > 0)
		{
			var idValues = ids.Select(x => x.Value);
			return parts with { Where = parts.Where.WithItem((IdColumn, Compare.In, idValues)) };
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort)
	{
		// Add random sort
		if (sortRandom)
		{
			return parts with { SortRandom = true };
		}

		// Add specific sort
		else if (sort.Count > 0)
		{
			return parts with { Sort = sort };
		}

		// Return
		return parts;
	}

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddWhere<TTable>(
		QueryParts parts,
		TTable table,
		Expression<Func<TTable, string>> column,
		Compare cmp,
		dynamic value
	)
		where TTable : ITable =>
		QueryF.GetColumnFromExpression(
			table, column
		)
		.Map(
			x => parts with { Where = parts.Where.WithItem((x, cmp, value)) },
			F.DefaultHandler
		);

	/// <inheritdoc/>
	public virtual Maybe<QueryParts> AddWhereCustom(QueryParts parts, string clause, object parameters)
	{
		// Check clause
		if (string.IsNullOrWhiteSpace(clause))
		{
			return F.None<QueryParts, M.TryingToAddEmptyClauseToWhereCustomMsg>();
		}

		// Get parameters
		var param = new QueryParametersDictionary();
		if (!param.TryAdd(parameters))
		{
			return F.None<QueryParts, M.UnableToAddParametersToWhereCustomMsg>();
		}

		// Add clause and return
		return parts with { WhereCustom = parts.WhereCustom.WithItem((clause, param)) };
	}
}
