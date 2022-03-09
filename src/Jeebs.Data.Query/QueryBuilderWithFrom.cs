// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Exceptions;
using Jeebs.Data.Query.Functions;
using Maybe;
using Maybe.Linq;

namespace Jeebs.Data.Query;

/// <inheritdoc cref="IQueryBuilderWithFrom"/>
public sealed record class QueryBuilderWithFrom : IQueryBuilderWithFrom
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
	internal Maybe<IQueryParts> Select<TModel>() =>
		from cols in Extract<TModel>.From(Tables.ToArray())
		select (IQueryParts)(Parts with { SelectColumns = cols });

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
		QueryJoin type,
		Expression<Func<TFrom, string>> fromColumn,
		Expression<Func<TTo, string>> toColumn
	)
		where TFrom : ITable, new()
		where TTo : ITable, new()
	{
		// Check 'from' table is already added
		CheckTable<TFrom, JoinFromTableNotAddedException<TFrom>>();

		// Add 'to' table to query
		AddTable<TTo>();

		// Get columns
		var from = QueryBuilderF.GetColumnFromExpression(fromColumn);
		var to = QueryBuilderF.GetColumnFromExpression(toColumn);

		// Add to query
		return type switch
		{
			QueryJoin.Inner =>
				this with
				{
					Parts = Parts with
					{
						InnerJoin = Parts.InnerJoin.WithItem((from, to))
					}
				},

			QueryJoin.Left =>
				this with
				{
					Parts = Parts with
					{
						LeftJoin = Parts.LeftJoin.WithItem((from, to))
					}
				},

			QueryJoin.Right =>
				this with
				{
					Parts = Parts with
					{
						RightJoin = Parts.RightJoin.WithItem((from, to))
					}
				},

			_ =>
				this
		};
	}

	/// <inheritdoc/>
	public IQueryBuilderWithFrom Where<TTable>(Expression<Func<TTable, string>> column, Compare cmp, object value)
		where TTable : ITable, new()
	{
		// Check table
		CheckTable<TTable, WhereTableNotAddedException<TTable>>();

		// Add predicate
		return this with
		{
			Parts = Parts with
			{
				Where = Parts.Where.WithItem((QueryBuilderF.GetColumnFromExpression(column), cmp, value))
			}
		};
	}

	/// <inheritdoc/>
	public IQueryBuilderWithFrom SortBy<TTable>(Expression<Func<TTable, string>> column)
		where TTable : ITable, new()
	{
		// Check table
		CheckTable<TTable, SortByTableNotAddedException<TTable>>();

		// Add sort column
		return this with
		{
			Parts = Parts with
			{
				Sort = Parts.Sort.WithItem((QueryBuilderF.GetColumnFromExpression(column), SortOrder.Ascending))
			}
		};
	}

	/// <inheritdoc/>
	public IQueryBuilderWithFrom SortByDescending<TTable>(Expression<Func<TTable, string>> column)
		where TTable : ITable, new()
	{
		// Check table
		CheckTable<TTable, SortByTableNotAddedException<TTable>>();

		// Add sort column
		return this with
		{
			Parts = Parts with
			{
				Sort = Parts.Sort.WithItem((QueryBuilderF.GetColumnFromExpression(column), SortOrder.Descending))
			}
		};
	}

	/// <inheritdoc/>
	public IQueryBuilderWithFrom Maximum(ulong number) =>
		this with { Parts = Parts with { Maximum = number } };

	/// <inheritdoc/>
	public IQueryBuilderWithFrom Skip(ulong number) =>
		this with { Parts = Parts with { Skip = number } };
}
