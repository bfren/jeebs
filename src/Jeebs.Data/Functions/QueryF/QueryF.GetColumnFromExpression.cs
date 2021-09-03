// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq.Expressions;
using Jeebs;
using Jeebs.Data.Mapping;
using Jeebs.Linq;

namespace F.DataF;

public static partial class QueryF
{
	/// <summary>
	/// Build a column object from a column selector expression
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	/// <param name="table">Table object</param>
	/// <param name="column">Column expression</param>
	public static Option<IColumn> GetColumnFromExpression<TTable>(TTable table, Expression<Func<TTable, string>> column)
		where TTable : ITable =>
		column.GetPropertyInfo()
		.Map<IColumn>(
			x => new Column(table, x.Get(table), x.Name),
			e => new Msg.UnableToGetColumnFromExpressionExceptionMsg(e)
		);

	/// <inheritdoc cref="GetColumnFromExpression{TTable}(TTable, Expression{Func{TTable, string}})"/>
	public static Option<IColumn> GetColumnFromExpression<TTable>(Expression<Func<TTable, string>> column)
		where TTable : ITable, new() =>
		GetColumnFromExpression(new TTable(), column);

	public static partial class Msg
	{
		/// <summary>Something went wrong while creating a column from the expression</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class UnableToGetColumnFromExpressionExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }
	}
}
