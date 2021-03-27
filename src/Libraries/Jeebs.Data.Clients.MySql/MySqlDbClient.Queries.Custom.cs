// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Text;
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

		/// <inheritdoc/>
		public override Option<(string query, IQueryParameters param)> GetQuery(IQueryParts parts)
		{
			// Convert select columns into joined list
			string getSelect()
			{
				var select = new List<string>();
				foreach (var column in parts.Select)
				{
					select.Add($"{Escape(column)} AS '{column.Alias}'");
				}

				return JoinList(select, false);
			}

			// Start query
			var select = (parts.Select.Count > 0) switch
			{
				true =>
					getSelect(),

				false =>
					"*"
			};

			var sql = new StringBuilder($"SELECT {select} FROM {Escape(parts.From)}");

			// Add INNER JOIN
			foreach (var (on, equals) in parts.InnerJoin)
			{
				sql.Append($" INNER JOIN {Escape(on.Table)} ON {Escape(on.Name, on.Table)} = {Escape(equals.Name, equals.Table)}");
			}

			// Add LEFT JOIN
			foreach (var (on, equals) in parts.LeftJoin)
			{
				sql.Append($" LEFT JOIN {Escape(on.Table)} ON {Escape(on.Name, on.Table)} = {Escape(equals.Name, equals.Table)}");
			}

			// Add RIGHT JOIN
			foreach (var (on, equals) in parts.RightJoin)
			{
				sql.Append($" RIGHT JOIN {Escape(on.Table)} ON {Escape(on.Name, on.Table)} = {Escape(equals.Name, equals.Table)}");
			}

			// Add WHERE
			IQueryParameters param = new QueryParameters();
			if (parts.Where.Count > 0)
			{
				var where = GetWhereAndParameters(parts.Where, true);
				sql.Append($" WHERE {JoinList(where.where, false)}");

				param = where.param;
			}

			// Add ORDER BY
			if (parts.OrderBy.Count > 0)
			{
				var orderBy = new List<string>();
				foreach (var (column, order) in parts.OrderBy)
				{
					orderBy.Add($"{Escape(column.Name, column.Table)} {order.ToOperator()}");
				}

				sql.Append($" ORDER BY {JoinList(orderBy, false)}");
			}

			// Add LIMIT
			if (parts.Maximum > 0)
			{
				// Add OFFSET
				if (parts.Skip > 0)
				{
					sql.Append($" LIMIT {parts.Skip}, {parts.Maximum}");
				}
				else
				{
					sql.Append($" LIMIT {parts.Maximum}");
				}
			}

			// Append semi-colon
			sql.Append(';');

			// Return query string
			return (sql.ToString(), param);
		}
	}
}
