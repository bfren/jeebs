// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Querying.Exceptions;

namespace F.DataF
{
	public static class QueryBuilderF
	{
		/// <summary>
		/// Build a column object from a column selector expression
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Column expression</param>
		public static IColumn GetColumn<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new() =>
			QueryF.GetColumnFromExpression(column).Unwrap(() => throw new UnableToGetColumnFromExpressionException<TTable>());
	}
}
