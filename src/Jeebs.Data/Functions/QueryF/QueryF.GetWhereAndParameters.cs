// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections;
using System.Collections.Generic;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;

namespace F.DataF
{
	public static partial class QueryF
	{
		/// <summary>
		/// Turn list of predicates into WHERE clauses with associated parameters
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="predicates">List of predicates</param>
		/// <param name="includeTableName">If true, column names will be namespaced with the table name (necessary in JOIN queries)</param>
		public static (IImmutableList<string> where, IQueryParameters param) GetWhereAndParameters(
			IDbClient client,
			IImmutableList<(IColumn column, Compare cmp, object value)> predicates,
			bool includeTableName
		)
		{
			// Create empty lists
			var where = new List<string>();
			var param = new QueryParameters();
			var index = 0;

			// Loop through predicates and add each one
			foreach (var (column, cmp, value) in predicates)
			{
				// Escape column with or without table
				var escapedColumn = includeTableName switch
				{
					true =>
						client.Escape(column.Name, column.Table),

					false =>
						client.Escape(column)
				};

				// IN is a special case, handle ordinary cases first
				if (cmp != Compare.In && cmp != Compare.NotIn)
				{
					var paramName = $"P{index++}";
					param.Add(paramName, value);

					where.Add($"{escapedColumn} {client.GetOperator(cmp)} {client.GetParamRef(paramName)}");

					continue;
				}

				// IN requires value to be a list of items
				if (value is not string && value is IEnumerable list)
				{
					// Add a parameter for each value
					var inParam = new List<string>();
					foreach (var inValue in list)
					{
						var inParamName = $"P{index++}";

						param.Add(inParamName, inValue);
						inParam.Add(client.GetParamRef(inParamName));
					}

					// If there are IN parameters, add to the query
					if (inParam.Count > 0)
					{
						where.Add($"{escapedColumn} {client.GetOperator(cmp)} {client.JoinList(inParam, true)}");
					}
				}
			}

			// Return
			return (ImmutableList.Create(items: where), param);
		}
	}
}
