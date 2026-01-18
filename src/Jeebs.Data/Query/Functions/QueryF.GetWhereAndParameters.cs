// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Generic;
using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;

namespace Jeebs.Data.Query.Functions;

public static partial class QueryF
{
	/// <summary>
	/// Turn list of predicates into WHERE clauses with associated parameters.
	/// </summary>
	/// <param name="client">IDbClient</param>
	/// <param name="predicates">List of predicates</param>
	/// <param name="includeTableName">If true, column names will be namespaced with the table name (necessary in JOIN queries)</param>
	public static (IImmutableList<string> where, IQueryParametersDictionary param) GetWhereAndParameters(
		IDbClient client,
		IImmutableList<(IColumn column, Compare compare, dynamic value)> predicates,
		bool includeTableName
	)
	{
		// Create empty lists
		var where = new List<string>();
		var param = new QueryParametersDictionary();
		var index = 0;

		// Loop through predicates and add each one
		foreach (var (column, compare, value) in predicates)
		{
			// Escape column with or without table
			var escapedColumn = includeTableName switch
			{
				true =>
					client.Escape(column.TblName, column.ColName),

				false =>
					client.Escape(column)
			};

			// IS is a special case for use with null
			if (compare is Compare.Is or Compare.IsNot && value is DBNull)
			{
				// Add null clause
				where.Add($"{escapedColumn} {client.GetOperator(compare)} NULL");

				// Move to next predicate
				continue;
			}

			// IN is a special case, handle ordinary cases first
			if (compare is not Compare.In and not Compare.NotIn)
			{
				// Auto-increment name and get parameter value (to support StrongId)
				var paramName = $"P{index++}";
				var paramValue = GetParameterValue(value);

				// Add parameter
				param.Add(paramName, paramValue);
				where.Add($"{escapedColumn} {client.GetOperator(compare)} {client.GetParamRef(paramName)}");

				// Move to next predicate
				continue;
			}

			// IN requires value to be a list of items
			if (value is not string and IEnumerable list)
			{
				// Add a parameter for each value
				var inParam = new List<string>();
				foreach (var inValue in list)
				{
					// Auto-increment namd and get parameter value (to support StrongId)
					var inParamName = $"P{index++}";
					var inParamValue = GetParameterValue(inValue);

					// Add parameter
					param.Add(inParamName, inParamValue);
					inParam.Add(client.GetParamRef(inParamName));
				}

				// If there are IN parameters, add to the query
				if (inParam.Count > 0)
				{
					where.Add($"{escapedColumn} {client.GetOperator(compare)} {client.JoinList(inParam, true)}");
				}
			}
		}

		// Return
		return (ImmutableList.Create(items: where), param);
	}
}
