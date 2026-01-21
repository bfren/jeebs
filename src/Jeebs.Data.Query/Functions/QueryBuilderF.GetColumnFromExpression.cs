// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryBuilderF
{
	/// <summary>
	/// Build a column object from a column selector expression.
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="column">Column expression.</param>
	public static IColumn GetColumnFromExpression<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		QueryF.GetColumnFromExpression(table, column).Unwrap();

	/// <inheritdoc cref="GetColumnFromExpression{TTable}(TTable, Expression{Func{TTable, string}})"/>
	public static IColumn GetColumnFromExpression<TTable>(Expression<Func<TTable, string>> column)
		where TTable : ITable, new() =>
		GetColumnFromExpression(new TTable(), column);
}
