// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs;
using Jeebs.Data;
using Jeebs.Linq;
using static F.OptionF;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Build a column object from a column selector expression
		/// </summary>
		/// <typeparam name="TTable">Table type</typeparam>
		/// <param name="column">Column expression</param>
		public static Option<IColumn> GetColumn<TTable>(Expression<Func<TTable, string>> column)
			where TTable : ITable, new()
		{
			// Get property info
			var info = column.GetPropertyInfo();
			if (info == null)
			{
				return None<IColumn, Msg.UnableToGetColumnFromExpressionMsg>();
			}

			// Create column
			var table = new TTable();
			return new Column(table.GetName(), info.Get(table), info.Name);
		}

		public static partial class Msg
		{
			/// <summary>IN predicate means value is 'in' an array of values so the value must be a list</summary>
			public sealed record UnableToGetColumnFromExpressionMsg : IMsg { }
		}
	}
}
