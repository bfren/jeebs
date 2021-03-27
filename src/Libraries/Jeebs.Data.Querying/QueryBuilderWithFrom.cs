// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.Exceptions;
using Jeebs.Linq;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryBuilderWithFrom"/>
	public sealed record QueryBuilderWithFrom : QueryBuilder, IQueryBuilderWithFrom
	{
		/// <summary>
		/// Query Parts
		/// </summary>
		private QueryParts Parts { get; init; }

		/// <summary>
		/// List of tables added to this query
		/// </summary>
		private List<ITable> Tables { get; init; }

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
		internal Option<QueryBuilderWithSelect> Select<TModel>() =>
			from columns in Extract<TModel>.From(Tables.ToArray())
			select new QueryBuilderWithSelect(Parts with { Select = columns });

		/// <summary>
		/// Verify that a table has been added to the list of tables
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <typeparam name="TException">Exception type to throw if the table has not been added</typeparam>
		private void CheckTable<TTable, TException>()
			where TTable : ITable, new()
			where TException : QueryBuilderException<TTable>, new()
		{
			if (!Tables.Contains(new TTable()))
			{
				throw new TException();
			}
		}

		/// <summary>
		/// Add a table to the list of tables, if it has not already been added
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		private void AddTable<TTable>()
			where TTable : ITable, new()
		{
			var table = new TTable();
			if (!Tables.Contains(table))
			{
				Tables.Add(table);
			}
		}

		/// <summary>
		/// Build a column object from a column selector expression
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Column expression</param>
		private static IColumn GetColumn<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new()
		{
			// Get property info
			var info = column.GetPropertyInfo();
			if (info == null)
			{
				throw new UnableToGetColumnFromExpressionException<TTable>();
			}

			// Create column
			var table = new TTable();
			return new Column(table.GetName(), info.Get(table), info.Name);
		}

		/// <inheritdoc/>
		public IQueryBuilderWithFrom Join<TFrom, TTo>(
			QueryJoin join,
			Expression<Func<TFrom, string>> on,
			Expression<Func<TTo, string>> equals
		)
			where TFrom : ITable, new()
			where TTo : ITable, new()
		{
			// Check 'from' table
			CheckTable<TFrom, JoinFromTableNotAddedException<TFrom>>();

			// Add 'to' table to query
			AddTable<TTo>();

			// Get columns
			var onColumn = GetColumn(on);
			var equalsColumn = GetColumn(equals);

			// Add to query
			switch (join)
			{
				case QueryJoin.Inner:
					Parts.InnerJoin.Add((onColumn, equalsColumn));
					break;

				case QueryJoin.Left:
					Parts.LeftJoin.Add((onColumn, equalsColumn));
					break;

				case QueryJoin.Right:
					Parts.RightJoin.Add((onColumn, equalsColumn));
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
			Parts.Where.Add((GetColumn(column), op, value));

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
			Parts.OrderBy.Add((GetColumn(column), SortOrder.Ascending));

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
			Parts.OrderBy.Add((GetColumn(column), SortOrder.Descending));

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
