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
		public MySqlAdapter() : base('.', ',', ',', '`', '`', "AS", '\'', '\'', "ASC", "DESC") { }

		/// <inheritdoc/>
		public override string GetRandomSortOrder() =>
			"RAND()";

		/// <inheritdoc/>
		public override string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases)
		{
			// Perform checks
			CreateSingleAndReturnIdChecks(table, columns, aliases);

			// Add @ to aliases (for use as parameters)
			var aliasesAtted = new List<string>();
			foreach (var item in aliases)
			{
				aliasesAtted.Add($"@{item}");
			}

			// Create separated columns and values strings
			var cols = Join(columns);
			var vals = Join(aliasesAtted);

			// Return query string
			return $"INSERT INTO {table} ({cols}) VALUES ({vals}); SELECT LAST_INSERT_ID();";
		}

		/// <inheritdoc/>
		public override string Retrieve(IQueryParts parts)
		{
			// Perform checks
			RetrieveChecks(parts);

			// Start query
			var select = "*";
			if (!string.IsNullOrWhiteSpace(parts.Select))
			{
				select = parts.Select;
			}

			var sql = new StringBuilder($"SELECT {select} FROM {parts.From}");

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
				sql.Append($" ORDER BY {Join(orderByValue)}");
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
		public override string RetrieveSingleById(string table, List<string> columns, string idColumn, string? idAlias = null)
		{
			// Set default ID Alias
			idAlias ??= nameof(IEntity.Id);

			// Perform checks
			RetrieveSingleByIdChecks(table, columns, idColumn, idAlias);

			// Return query string
			return $"SELECT {Join(columns)} FROM {table} WHERE {idColumn} = @{idAlias};";
		}

		/// <inheritdoc/>
		public override string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			// Perform checks
			UpdateSingleChecks(table, columns, aliases, idColumn, idAlias);

			// Add each column to the update list
			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			// Build SQL
			var sql = $"UPDATE {table} SET {Join(update)} WHERE {idColumn} = @{idAlias}";

			// Add Version column
			if (versionColumn is string column && versionAlias is string alias)
			{
				sql += $" AND {column} = @{alias} - 1";
			}

			// Return query string
			return $"{sql};";
		}

		/// <inheritdoc/>
		public override string DeleteSingle(string table, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			// Perform checks
			DeleteSingleChecks(table, idColumn, idAlias);

			// Build SQL
			var sql = $"DELETE FROM {table} WHERE {idColumn} = @{idAlias}";

			// Add Version column
			if (versionColumn is string column && versionAlias is string alias)
			{
				sql += $" AND {column} = @{alias} - 1";
			}

			// Return query string
			return $"{sql};";
		}
	}
}