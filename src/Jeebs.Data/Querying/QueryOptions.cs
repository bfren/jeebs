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
	/// <inheritdoc cref="IQueryOptions{TId}"/>
	public abstract record QueryOptions
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

	/// <inheritdoc cref="IQueryOptions{TId}"/>
	public abstract record QueryOptions<TId> : QueryOptions, IQueryOptions<TId>
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
		public long? Maximum { get; init; } = 10;

		/// <inheritdoc/>
		public long Skip { get; init; }

		/// <inheritdoc/>
		public virtual Option<IQueryParts> GetParts<TModel>() =>
			from map in GetMap()
			from cols in GetColumns<TModel>(map.table)
			from parts in GetParts(map.table, cols, map.idColumn)
			select (IQueryParts)parts;

		/// <summary>
		/// Get mapped Table and ID Column
		/// </summary>
		protected abstract Option<(ITable table, IColumn idColumn)> GetMap();

		/// <summary>
		/// Get select columns for the specified <typeparamref name="TModel"/>
		/// </summary>
		/// <typeparam name="TModel">Model type to use for selecting columns</typeparam>
		/// <param name="table">Select columns from this table</param>
		protected virtual Option<IColumnList> GetColumns<TModel>(ITable table) =>
			Extract<TModel>.From(table);

		/// <summary>
		/// Actually get the various query parts using helper functions
		/// </summary>
		/// <param name="table">Primary table</param>
		/// <param name="cols">Select ColumnList</param>
		/// <param name="idColumn">ID Column</param>
		protected virtual Option<QueryParts> GetParts(ITable table, IColumnList cols, IColumn idColumn) =>
			CreateParts(
				table, cols
			)
			.SwitchIf(
				_ => Id is not null || Ids is not null,
				x => AddWhereId(x, idColumn)
			)
			.SwitchIf(
				_ => SortRandom || Sort is not null,
				AddSort
			);

		/// <summary>
		/// Create a new QueryParts object, adding <paramref name="select"/> columns and
		/// <see cref="Maximum"/> and <see cref="Skip"/> values
		/// </summary>
		/// <param name="table">Primary table</param>
		/// <param name="select">Columns to select</param>
		protected virtual Option<QueryParts> CreateParts(ITable table, IColumnList select) =>
			new QueryParts(table)
			{
				Select = select,
				Maximum = Maximum,
				Skip = Skip
			};

		/// <summary>
		/// Add Inner Join
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="from">From table - should already be added to the query</param>
		/// <param name="to">To table</param>
		protected virtual Option<QueryParts> AddInnerJoin<TFrom, TTo>(
			QueryParts parts,
			(TFrom table, Expression<Func<TFrom, string>> column) from,
			(TTo table, Expression<Func<TTo, string>> column) to
		)
			where TFrom : ITable
			where TTo : ITable =>
			from colFrom in GetColumnFromExpression(@from.table, @from.column)
			from colTo in GetColumnFromExpression(to.table, to.column)
			select parts with { InnerJoin = parts.InnerJoin.With((colFrom, colTo)) };

		/// <summary>
		/// Add Left Join
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="from">From table - should already be added to the query</param>
		/// <param name="to">To table</param>
		protected virtual Option<QueryParts> AddLeftJoin<TFrom, TTo>(
			QueryParts parts,
			(TFrom table, Expression<Func<TFrom, string>> column) from,
			(TTo table, Expression<Func<TTo, string>> column) to
		)
			where TFrom : ITable
			where TTo : ITable =>
			from colFrom in GetColumnFromExpression(@from.table, @from.column)
			from colTo in GetColumnFromExpression(to.table, to.column)
			select parts with { LeftJoin = parts.LeftJoin.With((colFrom, colTo)) };

		/// <summary>
		/// Add Right Join
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="from">From table - should already be added to the query</param>
		/// <param name="to">To table</param>
		protected virtual Option<QueryParts> AddRightJoin<TFrom, TTo>(
			QueryParts parts,
			(TFrom table, Expression<Func<TFrom, string>> column) from,
			(TTo table, Expression<Func<TTo, string>> column) to
		)
			where TFrom : ITable
			where TTo : ITable =>
			from colFrom in GetColumnFromExpression(@from.table, @from.column)
			from colTo in GetColumnFromExpression(to.table, to.column)
			select parts with { RightJoin = parts.RightJoin.With((colFrom, colTo)) };

		/// <summary>
		/// Add Id / Ids - Id takes precedence over Ids
		/// </summary>
		/// <param name="parts">QueryParts</param>
		/// <param name="idColumn">ID Column</param>
		protected virtual Option<QueryParts> AddWhereId(QueryParts parts, IColumn idColumn)
		{
			// Add Id EQUAL
			if (Id is not null)
			{
				return parts with { Where = parts.Where.With((idColumn, Compare.Equal, Id.Value)) };
			}

			// Add Id IN
			else if (Ids is not null)
			{
				var ids = Ids.Select(x => x.Value);
				return parts with { Where = parts.Where.With((idColumn, Compare.In, ids)) };
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
			.Map(
				x => parts with { Where = parts.Where.With((x, cmp, value)) },
				DefaultHandler
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

		#region Testing

		internal Option<QueryParts> GetPartsTest(ITable table, IColumnList cols, IColumn idColumn) =>
			GetParts(table, cols, idColumn);

		internal Option<QueryParts> CreatePartsTest(ITable table, IColumnList select) =>
			CreateParts(table, select);

		internal Option<QueryParts> AddWhereIdTest(QueryParts parts, IColumn idColumn) =>
			AddWhereId(parts, idColumn);

		internal Option<QueryParts> AddSortTest(QueryParts parts) =>
			AddSort(parts);

		internal Option<QueryParts> AddWhereTest<TTable>(
			QueryParts parts,
			TTable table,
			Expression<Func<TTable, string>> column,
			Compare cmp,
			object value
		) where TTable : ITable =>
			AddWhere(parts, table, column, cmp, value);

		internal Option<QueryParts> AddWhereCustomTest(QueryParts parts, string clause, object parameters) =>
			AddWhereCustom(parts, clause, parameters);

		#endregion
	}

	/// <inheritdoc cref="IQueryOptions{TId}"/>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract record QueryOptions<TEntity, TId> : QueryOptions<TId>, IQueryOptions<TId>
		where TEntity : IWithId<TId>
		where TId : StrongId
	{
		private readonly IMapper mapper;

		/// <summary>
		/// Create using default <see cref="IMapper"/>
		/// </summary>
		protected QueryOptions() : this(Mapper.Instance) { }

		/// <summary>
		/// Inject an <see cref="IMapper"/>
		/// </summary>
		/// <param name="mapper">IMapper</param>
		protected QueryOptions(IMapper mapper) =>
			this.mapper = mapper;

		/// <inheritdoc/>
		protected override Option<(ITable table, IColumn idColumn)> GetMap() =>
			from map in mapper.GetTableMapFor<TEntity>()
			select (map.Table, (IColumn)map.IdColumn);
	}
}
