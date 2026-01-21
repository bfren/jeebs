// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Map;
using Jeebs.Reflection;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Build a column object from a column selector expression.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="column">Column expression.</param>
	public static Result<IColumn> GetColumnFromExpression<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		(
			from i in column.GetPropertyInfo()
			from t in i.Get(table)
			select (IColumn)new Column(table.GetName(), t, i.Info)
		)
		.ToResult(
			() => R.Fail(nameof(QueryF), nameof(GetColumnFromExpression),
				"Unable to get column from expression for table '{Table}'.", table
			)
		);

	/// <inheritdoc cref="GetColumnFromExpression{TTable}(TTable, Expression{Func{TTable, string}})"/>
	public static Result<IColumn> GetColumnFromExpression<TTable>(Expression<Func<TTable, string>> column)
		where TTable : ITable, new() =>
		GetColumnFromExpression(
			new TTable(), column
		);
}
