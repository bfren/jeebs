// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Linq;
using static F.DataF.QueryF;
using static F.OptionF;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="QueryPartsBuilder{TId}"/>
	public abstract class QueryPartsBuilder
	{
		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Trying to add an empty where clause</summary>
			public sealed record TryingToAddEmptyClauseToWhereCustomMsg : IMsg { }

			/// <summary>Unable to add parameters to custom where</summary>
			public sealed record UnableToAddParametersToWhereCustomMsg : IMsg { }
		}
	}

	/// <summary>
	/// Builds a <see cref="QueryParts"/> object from various options
	/// </summary>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract class QueryPartsBuilder<TId> : QueryPartsBuilder, IQueryPartsBuilder<TId>
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
		public virtual Option<QueryParts> Create(ITable table, IColumnList select, long? maximum, long skip) =>
			new QueryParts(table)
			{
				Select = select,
				Maximum = maximum,
				Skip = skip
			};

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
		protected internal Option<QueryParts> AddJoin<TFrom, TTo>(
			QueryParts parts,
			TFrom fromTable,
			Expression<Func<TFrom, string>> fromSelector,
			TTo toTable,
			Expression<Func<TTo, string>> toSelector,
			Func<QueryParts, IColumn, IColumn, QueryParts> withJoin
		)
			where TFrom : ITable
			where TTo : ITable
		{
			return from colFrom in GetColumnFromExpression(fromTable, fromSelector)
				   from colTo in GetColumnFromExpression(toTable, toSelector)
				   select withJoin(parts, colFrom, colTo);
		}

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		public virtual Option<QueryParts> AddInnerJoin<TFrom, TTo>(
			QueryParts parts,
			TFrom fromTable,
			Expression<Func<TFrom, string>> fromSelector,
			TTo toTable,
			Expression<Func<TTo, string>> toSelector
		)
			where TFrom : ITable
			where TTo : ITable
		{
			return AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
				parts with { InnerJoin = parts.InnerJoin.With((colFrom, colTo)) }
			);
		}

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		public virtual Option<QueryParts> AddLeftJoin<TFrom, TTo>(
			QueryParts parts,
			TFrom fromTable,
			Expression<Func<TFrom, string>> fromSelector,
			TTo toTable,
			Expression<Func<TTo, string>> toSelector
		)
			where TFrom : ITable
			where TTo : ITable
		{
			return AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
				parts with { LeftJoin = parts.LeftJoin.With((colFrom, colTo)) }
			);
		}

		/// <inheritdoc cref="QueryOptions.AddJoin"/>
		public virtual Option<QueryParts> AddRightJoin<TFrom, TTo>(
			QueryParts parts,
			TFrom fromTable,
			Expression<Func<TFrom, string>> fromSelector,
			TTo toTable,
			Expression<Func<TTo, string>> toSelector
		)
			where TFrom : ITable
			where TTo : ITable
		{
			return AddJoin(parts, fromTable, fromSelector, toTable, toSelector, (parts, colFrom, colTo) =>
				parts with { RightJoin = parts.RightJoin.With((colFrom, colTo)) }
			);
		}

		/// <summary>
		/// Add Id / Ids - Id takes precedence over Ids
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="idColumn">ID Column</param>
		/// <param name="id">Single ID</param>
		/// <param name="ids">List of IDs</param>
		public virtual Option<QueryParts> AddWhereId(QueryParts parts, IColumn idColumn, TId? id, IImmutableList<TId> ids)
		{
			// Add Id EQUAL
			if (id?.Value > 0)
			{
				return parts with { Where = parts.Where.With((idColumn, Compare.Equal, id.Value)) };
			}

			// Add Id IN
			else if (ids.Count > 0)
			{
				var idValues = ids.Select(x => x.Value);
				return parts with { Where = parts.Where.With((idColumn, Compare.In, idValues)) };
			}

			// Return
			return parts;
		}

		/// <summary>
		/// Add Sort - SortRandom takes precendence over Sort
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="sortRandom">If true, will sort results randomly</param>
		/// <param name="sort">Sort columns</param>
		public virtual Option<QueryParts> AddSort(QueryParts parts, bool sortRandom, IImmutableList<(IColumn, SortOrder)> sort)
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

		/// <summary>
		/// Add a Where predicate using Linq Expressions
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="parts">QueryParts</param>
		/// <param name="table">Table object</param>
		/// <param name="column">Column selector</param>
		/// <param name="cmp">Compare operator</param>
		/// <param name="value">Search value</param>
		public virtual Option<QueryParts> AddWhere<TTable>(
			QueryParts parts,
			TTable table,
			Expression<Func<TTable, string>> column,
			Compare cmp,
			object value
		)
			where TTable : ITable
		{
			return GetColumnFromExpression(
				table, column
			)
			.Map(
				x => parts with { Where = parts.Where.With((x, cmp, value)) },
				DefaultHandler
			);
		}

		/// <summary>
		/// Add a custom Where predicate
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="clause">Clause text</param>
		/// <param name="parameters">Clause parameters</param>
		public virtual Option<QueryParts> AddWhereCustom(QueryParts parts, string clause, object parameters)
		{
			// Check clause
			if (string.IsNullOrWhiteSpace(clause))
			{
				return None<QueryParts, Msg.TryingToAddEmptyClauseToWhereCustomMsg>();
			}

			// Get parameters
			var param = new QueryParameters();
			if (!param.TryAdd(parameters))
			{
				return None<QueryParts, Msg.UnableToAddParametersToWhereCustomMsg>();
			}

			// Add clause and return
			return parts with { WhereCustom = parts.WhereCustom.With((clause, param)) };
		}
	}
}
