// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;
using static F.DataF.QueryF;

namespace Jeebs.Data.Clients.SqlServer
{
	public partial class SqlServerDbClient : DbClient
	{
		/// <inheritdoc/>
		protected override (string query, IQueryParameters param) GetQuery(
			string table,
			ColumnList columns,
			List<(IColumn column, SearchOperator op, object value)> predicates
		)
		{
			// Get columns
			var col = GetColumnsFromList(this, columns);

			// Add each predicate to the where and parameter lists
			var (where, param) = GetWhereAndParameters(this, predicates, false);

			// Return query and parameters
			return (
				$"SELECT {JoinList(col, false)} FROM {Escape(table)} WHERE {string.Join(" AND ", where)}",
				param
			);
		}

		/// <inheritdoc/>
		public override (string query, IQueryParameters param) GetQuery(IQueryParts parts)
		{
			// Start query
			var select = (parts.Select.Count > 0) switch
			{
				true =>
					GetSelectFromList(this, parts.Select),

				false =>
					"*"
			};

			var sql = new StringBuilder($"SELECT {select} FROM {Escape(parts.From)}");

			// Add INNER JOIN
			foreach (var (from, to) in parts.InnerJoin)
			{
				sql.Append($" INNER JOIN {Escape(to.Table)} ON {Escape(from.Name, from.Table)} = {Escape(to.Name, to.Table)}");
			}

			// Add LEFT JOIN
			foreach (var (from, to) in parts.LeftJoin)
			{
				sql.Append($" LEFT JOIN {Escape(to.Table)} ON {Escape(from.Name, from.Table)} = {Escape(to.Name, to.Table)}");
			}

			// Add RIGHT JOIN
			foreach (var (from, to) in parts.RightJoin)
			{
				sql.Append($" RIGHT JOIN {Escape(to.Table)} ON {Escape(from.Name, from.Table)} = {Escape(to.Name, to.Table)}");
			}

			// Add WHERE
			IQueryParameters parameters = new QueryParameters();
			if (parts.Where.Count > 0)
			{
				var (where, param) = GetWhereAndParameters(this, parts.Where, true);
				sql.Append($" WHERE {string.Join(" AND ", where)}");

				parameters = param;
			}

			// Add ORDER BY
			if (parts.Sort.Count > 0)
			{
				var orderBy = new List<string>();
				foreach (var (column, order) in parts.Sort)
				{
					orderBy.Add($"{Escape(column.Name, column.Table)} {order.ToOperator()}");
				}

				sql.Append($" ORDER BY {JoinList(orderBy, false)}");
			}

			// Add OFFSET and FETCH
			if (parts.Maximum > 0 && parts.Skip >= 0)
			{
				sql.Append($" OFFSET {parts.Skip} ROWS");
				sql.Append($" FETCH NEXT {parts.Maximum} ROWS ONLY");
			}

			// Return query string
			return (
				sql.ToString(),
				parameters
			);
		}
	}
}
