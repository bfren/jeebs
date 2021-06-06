// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Linq;
using static F.OptionF;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Convert LINQ expression property selectors to column names
		/// </summary>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <param name="columns">Mapped entity columns</param>
		/// <param name="predicates">Predicates (matched using AND)</param>
		public static Option<List<(IColumn column, SearchOperator op, object value)>> ConvertPredicatesToColumns<TEntity>(
			IMappedColumnList columns,
			(Expression<Func<TEntity, object>> column, SearchOperator op, object value)[] predicates
		)
			where TEntity : IEntity
		{
			var list = new List<(IColumn, SearchOperator, object)>();
			foreach (var item in predicates)
			{
				// The property name is the column alias
				var alias = item.column.GetPropertyInfo().Switch<string?>(
					some: x => x.Name,
					none: () => null
				);

				if (alias is null)
				{
					continue;
				}

				// Retrieve column using alias
				var column = columns.SingleOrDefault(c => c.Alias == alias);
				if (column is null)
				{
					continue;
				}

				// If predicate is IN, make sure it is a list
				if ((item.op == SearchOperator.In || item.op == SearchOperator.NotIn)
					&& (item.value is not IEnumerable || item.value is string) // string implements IEnumerable but is not valid for IN
				)
				{
					return None<List<(IColumn, SearchOperator, object)>, Msg.InOperatorRequiresValueToBeAListMsg>();
				}

				// Add to list of predicates using column name
				list.Add((column, item.op, item.value));
			}

			return list;
		}

		public static partial class Msg
		{
			/// <summary>IN predicate means value is 'in' an array of values so the value must be a list</summary>
			public sealed record InOperatorRequiresValueToBeAListMsg : IMsg { }
		}
	}
}
