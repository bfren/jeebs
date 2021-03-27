// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		protected override (string query, IQueryParameters param) GetQuery(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"{Escape(column)} AS '{column.Alias}'");
			}

			// Add each predicate to the where and parameter lists
			var (where, param) = GetWhereAndParameters(predicates, false);

			// Return query and parameters
			return ($"SELECT {JoinList(col, false)} FROM {Escape(table)} WHERE {string.Join(" AND ", where)};", param);
		}
	}
}
