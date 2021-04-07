// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections;
using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Enums;

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
		public static (List<string> where, IQueryParameters param) GetWhereAndParameters(
			IDbClient client,
			List<(IColumn column, SearchOperator op, object value)> predicates,
			bool includeTableName
		)
		{
			// Create empty lists
			var where = new List<string>();
			var param = new QueryParameters();
			var index = 0;

			// Loop through predicates and add each one
			foreach (var (column, op, value) in predicates)
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
				if (op != SearchOperator.In && op != SearchOperator.NotIn)
				{
					var paramName = $"P{index++}";
					param.Add(paramName, value);

					where.Add($"{escapedColumn} {client.GetOperator(op)} {client.GetParamRef(paramName)}");

					continue;
				}

				// IN requires value to be a list of items
				if (value is IEnumerable list)
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
						where.Add($"{escapedColumn} {client.GetOperator(op)} {client.JoinList(inParam, true)}");
					}
				}
			}

			// Return
			return (where, param);
		}
	}
}
