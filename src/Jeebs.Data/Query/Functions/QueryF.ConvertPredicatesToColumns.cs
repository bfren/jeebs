// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Messages;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Convert LINQ expression property selectors to column names
	/// </summary>
	/// <param name="columns">Mapped entity columns</param>
	/// <param name="predicates">Predicates (matched using AND)</param>
	public static Maybe<IImmutableList<(IColumn column, Compare cmp, dynamic value)>> ConvertPredicatesToColumns(
		IMappedColumnList columns,
		(string alias, Compare cmp, dynamic value)[] predicates
	)
	{
		var list = new List<(IColumn, Compare, dynamic)>();
		foreach (var item in predicates)
		{
			// Column alias (aka the property name) is required
			if (item.alias is null)
			{
				continue;
			}

			// Retrieve column using alias
			var column = columns.SingleOrDefault(c => c.ColAlias == item.alias);
			if (column is null)
			{
				continue;
			}

			// If predicate is IN, make sure it is a list
			if ((item.cmp == Compare.In || item.cmp == Compare.NotIn)
				&& (item.value is not IEnumerable || item.value is string) // string implements IEnumerable but is not valid for IN
			)
			{
				return F.None<IImmutableList<(IColumn, Compare, dynamic)>, M.InOperatorRequiresValueToBeAListMsg>();
			}

			// Get parameter value (to support StrongId)
			var value = GetParameterValue(item.value);

			// Add to list of predicates using column name
			list.Add((column, item.cmp, value));
		}

		return ImmutableList.Create(items: list);
	}

	public static partial class M
	{
		/// <summary>IN predicate means value is 'in' an array of values so the value must be a list</summary>
		public sealed record class InOperatorRequiresValueToBeAListMsg : Msg;
	}
}
