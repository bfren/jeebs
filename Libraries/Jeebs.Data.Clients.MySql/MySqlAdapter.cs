using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc/>
	public sealed class MySqlAdapter : Adapter
	{
		/// <summary>
		/// Create object
		/// </summary>
		public MySqlAdapter() : base('.', ", ", '`', '`', "AS", '\'', '\'', "ASC", "DESC") { }

		/// <inheritdoc/>
		public override string GetRandomSortOrder()
			=> "RAND()";

		/// <inheritdoc/>
		public override string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases)
			=> string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT LAST_INSERT_ID();",
				table,
				string.Join(ColumnSeparator, columns),
				"@" + string.Join($"{ColumnSeparator}@", aliases)
			);

		/// <inheritdoc/>
		public override string Retrieve(IQueryParts parts)
		{
			// Make sure FROM is not null
			if (parts.From == null)
			{
				throw new InvalidOperationException($"{nameof(IQueryParts)} must have FROM set before using it to retrieve a query.");
			}

			// Start query
			StringBuilder sql = new StringBuilder($"SELECT {parts.Select ?? "*"} FROM {parts.From}");

			// Add INNER JOIN
			if (parts.InnerJoin is List<(string table, string on, string equals)> innerJoinValues)
			{
				foreach (var (table, on, equals) in innerJoinValues)
				{
					sql.Append($" INNER JOIN {table} ON {on} = {equals}");
				}
			}

			// Add LEFT JOIN
			if (parts.LeftJoin is List<(string table, string on, string equals)> leftJoinValues)
			{
				foreach (var (table, on, equals) in leftJoinValues)
				{
					sql.Append($" LEFT JOIN {table} ON {on} = {equals}");
				}
			}

			// Add RIGHT JOIN
			if (parts.RightJoin is List<(string table, string on, string equals)> rightJoinValues)
			{
				foreach (var (table, on, equals) in rightJoinValues)
				{
					sql.Append($" RIGHT JOIN {table} ON {on} = {equals}");
				}
			}

			// Add WHERE
			if (parts.Where is List<string> whereValue)
			{
				sql.Append($" WHERE {string.Join(" AND ", whereValue)}");
			}

			// Add ORDER BY
			if (parts.OrderBy is List<string> orderByValue)
			{
				sql.Append($" ORDER BY {string.Join(ColumnSeparator, orderByValue)}");
			}

			// Add LIMIT
			if (parts.Limit is long limitValue && limitValue > 0)
			{
				// Add OFFSET
				if (parts.Offset is long offsetValue && offsetValue > 0)
				{
					sql.Append($" LIMIT {offsetValue}, {limitValue}");
				}
				else
				{
					sql.Append($" LIMIT {limitValue}");
				}
			}

			// Append semi-colon
			sql.Append(";");

			// Return query string
			return sql.ToString();
		}

		/// <inheritdoc/>
		public override string RetrieveSingleById(List<string> columns, string table, string idColumn)
			=> string.Format("SELECT {0} FROM {1} WHERE {2} = @Id;",
				string.Join(ColumnSeparator, columns),
				table,
				idColumn
			);

		/// <inheritdoc/>
		public override string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			// Add each column to the update list
			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			// Build SQL
			var sql = string.Format("UPDATE {0} SET {1} WHERE {2} = @{3}",
				table,
				string.Join(ColumnSeparator, update),
				idColumn,
				idAlias
			);

			// Add Version column
			if (versionColumn is string column && versionAlias is string alias)
			{
				sql += string.Format(" AND {0} = @{1} - 1", column, alias);
			}

			// Return SQL
			return $"{sql};";
		}

		/// <inheritdoc/>
		public override string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			// Build SQL
			var sql = string.Format("DELETE FROM {0} WHERE {1} = @{2}",
				table,
				idColumn,
				idAlias
			);

			// Add Version column
			if (versionColumn is string column && versionAlias is string alias)
			{
				sql += string.Format(" AND {0} = @{1} - 1", column, alias);
			}

			// Return SQL
			return $"{sql};";
		}
	}
}