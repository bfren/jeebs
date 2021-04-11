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
	/// <inheritdoc cref="IQueryOptions{TEntity, TId}"/>
	/// <param name="From">From table</param>
	public abstract record QueryOptions<TEntity, TId> : IQueryOptions<TEntity, TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		/// <inheritdoc/>
		public TId? Id { get; init; }

		/// <inheritdoc/>
		public TId[]? Ids { get; init; }

		/// <inheritdoc/>
		public (IColumn column, SortOrder order)[]? Sort { get; init; }

		/// <inheritdoc/>
		public bool SortRandom { get; init; }

		/// <inheritdoc/>
		public long? Maximum { get; init; }

		/// <inheritdoc/>
		public long Skip { get; init; }

		/// <inheritdoc/>
		public Option<IQueryParts> GetParts<TModel>() =>
			from map in Mapper.Instance.GetTableMapFor<TEntity>()
			from cols in GetColumns<TModel>(map)
			from parts in GetParts(map, cols)
			select (IQueryParts)parts;

		/// <summary>
		/// Get select columns for the specified <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type to use for selecting columns</typeparam>
		/// <param name="map">ITableMap for <typeparamref name="TEntity"/></param>
		protected virtual Option<IColumnList> GetColumns<TModel>(ITableMap map) =>
			Extract<TModel>.From(map.Table);

		/// <summary>
		/// Actually get the various query parts using helper functions
		/// </summary>
		/// <param name="map">ITableMap for <typeparamref name="TEntity"/></param>
		/// <param name="cols">Select ColumnList</param>
		protected virtual Option<QueryParts> GetParts(ITableMap map, IColumnList cols) =>
			CreateParts(
				map
			)
			.Bind(
				x => AddSelect(x, cols)
			)
			.Bind(
				x => AddId(x, map)
			)
			.Bind(
				x => AddSort(x)
			)
			.Map(
				x => x with
				{
					Maximum = Maximum,
					Skip = Skip
				},
				DefaultHandler
			);

		/// <summary>
		/// Create a new QueryParts object
		/// </summary>
		/// <param name="map">ITableMap for <typeparamref name="TEntity"/></param>
		protected virtual Option<QueryParts> CreateParts(ITableMap map) =>
			new QueryParts(map.Table);

		/// <summary>
		/// Add Select columns
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="cols">Select ColumnList</param>
		protected virtual Option<QueryParts> AddSelect(QueryParts parts, IColumnList cols) =>
			parts with { Select = cols };

		/// <summary>
		/// Add Id / Ids - Id takes precedence over Ids
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="map">ITableMap for <typeparamref name="TEntity"/></param>
		protected virtual Option<QueryParts> AddId(QueryParts parts, ITableMap map)
		{
			// Add Id EQUAL
			if (Id is not null)
			{
				return parts with { Where = parts.Where.With((map.IdColumn, Compare.Equal, Id)) };
			}

			// Add Id IN
			else if (Ids is not null)
			{
				return parts with { Where = parts.Where.With((map.IdColumn, Compare.In, Ids)) };
			}

			// Return
			return parts;
		}

		/// <summary>
		/// Add Sort - SortRandom takes precendence over Sort
		/// </summary>
		/// <param name="parts">QueryParts</param>
		protected virtual Option<QueryParts> AddSort(QueryParts parts)
		{
			// Add random sort
			if (SortRandom)
			{
				return parts with { SortRandom = true };
			}

			// Add specific sort
			else if (Sort is not null)
			{
				return parts with { Sort = Sort.ToImmutableList() };
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
		protected virtual Option<QueryParts> AddWhere<TTable>(
			QueryParts parts,
			TTable table,
			Expression<Func<TTable, string>> column,
			Compare cmp,
			object value
		) where TTable : ITable =>
			GetColumnFromExpression(
				table, column
			)
			.Switch(
				x => Return(parts with { Where = parts.Where.With((x, cmp, value)) }),
				r => None<QueryParts>(r)
			);

		/// <summary>
		/// Add a custom Where predicate
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="clause">Clause text</param>
		/// <param name="parameters">Clause parameters</param>
		protected virtual Option<QueryParts> AddWhereCustom(QueryParts parts, string clause, object parameters)
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

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Trying to add an empty where clause</summary>
			public sealed record TryingToAddEmptyClauseToWhereCustomMsg : IMsg { }

			/// <summary>Unable to add parameters to custom where</summary>
			public sealed record UnableToAddParametersToWhereCustomMsg : IMsg { }
		}
	}
}
