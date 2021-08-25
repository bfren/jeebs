// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
			public sealed record class TryingToAddEmptyClauseToWhereCustomMsg : IMsg { }

			/// <summary>Unable to add parameters to custom where</summary>
			public sealed record class UnableToAddParametersToWhereCustomMsg : IMsg { }
		}
	}

	/// <summary>
	/// Builds a <see cref="QueryParts"/> object from various options
	/// </summary>
	/// <typeparam name="TId">Entity ID type</typeparam>
	public abstract class QueryPartsBuilder<TId> : QueryPartsBuilder, IQueryPartsBuilder<TId>
		where TId : IStrongId
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
				Select = GetColumns<TModel>(),
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public virtual Option<QueryParts> AddWhereId(QueryParts parts, TId? id, IImmutableList<TId> ids)
		{
			// Add Id EQUAL
			if (id?.Value > 0)
			{
				return parts with { Where = parts.Where.With((IdColumn, Compare.Equal, id.Value)) };
			}

			// Add Id IN
			else if (ids.Count > 0)
			{
				var idValues = ids.Select(x => x.Value);
				return parts with { Where = parts.Where.With((IdColumn, Compare.In, idValues)) };
			}

			// Return
			return parts;
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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
