// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.Exceptions;
using Jeebs.Linq;
using static F.DataF.QueryBuilderF;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilderWithFrom"/>
	public sealed record QueryBuilderWithFrom : IQueryBuilderWithFrom
	{
		/// <summary>
		/// Query Parts
		/// </summary>
		internal QueryParts Parts { get; private init; }

		/// <summary>
		/// List of tables added to this query
		/// </summary>
		internal List<ITable> Tables { get; private init; }

		/// <summary>
		/// Create using the specified table
		/// </summary>
		/// <param name="from">'From' table</param>
		internal QueryBuilderWithFrom(ITable from)
		{
			Parts = new QueryParts(from);
			Tables = new(new[] { from });
		}

		/// <summary>
		/// Select matching columns based on the specified model
		/// </summary>
		/// <typeparam name="TModel">Model type</typeparam>
		internal Option<IQueryParts> Select<TModel>() =>
			from columns in Extract<TModel>.From(Tables.ToArray())
			select (IQueryParts)(Parts with { Select = columns });

		/// <summary>
		/// Verify that a table has been added to the list of tables
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <typeparam name="TException">Exception type to throw if the table has not been added</typeparam>
		internal void CheckTable<TTable, TException>()
			where TTable : ITable, new()
			where TException : QueryBuilderException<TTable>, new()
		{
			if (Tables.Any(t => t is TTable))
			{
				return;
			}

			throw new TException();
		}

		/// <summary>
		/// Add a table to the list of tables, if it has not already been added
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		internal void AddTable<TTable>()
			where TTable : ITable, new()
		{
			var table = new TTable();
			if (!Tables.Any(t => t is TTable))
			{
				Tables.Add(table);
			}
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom Join<TFrom, TTo>(
			QueryJoin join,
			Expression<Func<TFrom, string>> from,
			Expression<Func<TTo, string>> to
		)
			where TFrom : ITable, new()
			where TTo : ITable, new()
		{
			// Check 'from' table is already added
			CheckTable<TFrom, JoinFromTableNotAddedException<TFrom>>();

			// Add 'to' table to query
			AddTable<TTo>();

			// Get columns
			var fromColumn = GetColumnFromExpression(from);
			var toColumn = GetColumnFromExpression(to);

			// Add to query
			switch (join)
			{
				case QueryJoin.Inner:
					Parts.InnerJoin.Add((fromColumn, toColumn));
					break;

				case QueryJoin.Left:
					Parts.LeftJoin.Add((fromColumn, toColumn));
					break;

				case QueryJoin.Right:
					Parts.RightJoin.Add((fromColumn, toColumn));
					break;
			}

			// Return
			return this;
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom Where<TTable>(Expression<Func<TTable, string>> column, SearchOperator op, object value)
			where TTable : ITable, new()
		{
			// Check table
			CheckTable<TTable, WhereTableNotAddedException<TTable>>();

			// Add predicate
			Parts.Where.Add((GetColumnFromExpression(column), op, value));

			// Return
			return this;
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom SortBy<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new()
		{
			// Check table
			CheckTable<TTable, SortByTableNotAddedException<TTable>>();

			// Add sort column
			Parts.Sort.Add((GetColumnFromExpression(column), SortOrder.Ascending));

			// Return
			return this;
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom SortByDescending<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new()
		{
			// Check table
			CheckTable<TTable, SortByTableNotAddedException<TTable>>();

			// Add sort column
			Parts.Sort.Add((GetColumnFromExpression(column), SortOrder.Descending));

			// Return
			return this;
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom Maximum(long max) =>
			this with { Parts = Parts with { Maximum = max } };

		/// <inheritdoc/>
		public IQueryBuilderWithFrom Skip(long skip) =>
			this with { Parts = Parts with { Skip = skip } };
	}
}
