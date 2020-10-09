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
		public override string GetRandomSortOrder()
			=> "RAND()";

		/// <inheritdoc/>
		public override string CreateSingleAndReturnId(string table, List<string> columns, List<string> aliases)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle empty aliases
			if (aliases.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(aliases)} cannot be empty.");
			}

			// Columns and aliases must contain the same number of items
			if (columns.Count != aliases.Count)
			{
				throw new InvalidOperationException($"The number of {nameof(columns)} ({columns.Count}) and {nameof(aliases)} ({aliases.Count}) must be the same.");
			}

			// Add @ to aliases (for use as parameters)
			var aliasesAtted = new List<string>();
			foreach (var item in aliases)
			{
				aliasesAtted.Add($"@{item}");
			}

			// Create separated columns and values strings
			var cols = JoinColumns(columns);
			var vals = JoinColumns(aliasesAtted);

			// Return query string
			return $"INSERT INTO {table} ({cols}) VALUES ({vals}); SELECT LAST_INSERT_ID();";
		}

		/// <inheritdoc/>
		public override string Retrieve(IQueryParts parts)
		{
			// Make sure FROM is not empty
			if (IsInvalidIdentifier(parts.From))
			{
				throw new InvalidOperationException($"Table is invalid: '{parts.From}'.");
			}

			// Start query
			var select = "*";
			if (!string.IsNullOrWhiteSpace(parts.Select))
			{
				select = parts.Select;
			}

			StringBuilder sql = new StringBuilder($"SELECT {select} FROM {parts.From}");

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
				sql.Append($" ORDER BY {JoinColumns(orderByValue)}");
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
		public override string RetrieveSingleById(List<string> columns, string table, string idColumn, string? idAlias = null)
		{
			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID alias
			idAlias ??= nameof(IEntity.Id);
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}

			return $"SELECT {JoinColumns(columns)} FROM {table} WHERE {idColumn} = @{idAlias};";
		}

		/// <inheritdoc/>
		public override string UpdateSingle(string table, List<string> columns, List<string> aliases, string idColumn, string idAlias, string? versionColumn = null, string? versionAlias = null)
		{
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle empty columns
			if (columns.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(columns)} cannot be empty.");
			}

			// Handle empty aliases
			if (aliases.Count == 0)
			{
				throw new InvalidOperationException($"The list of {nameof(aliases)} cannot be empty.");
			}

			// Columns and aliases must contain the same number of items
			if (columns.Count != aliases.Count)
			{
				throw new InvalidOperationException($"The number of {nameof(columns)} ({columns.Count}) and {nameof(aliases)} ({aliases.Count}) must be the same.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID Alias
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}

			// Add each column to the update list
			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			// Build SQL
			var sql = $"UPDATE {table} SET {JoinColumns(update, true)} WHERE {idColumn} = @{idAlias}";

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
			// Handle invalid table
			if (IsInvalidIdentifier(table))
			{
				throw new InvalidOperationException($"Table is invalid: '{table}'.");
			}

			// Handle invalid ID column
			if (IsInvalidIdentifier(idColumn))
			{
				throw new InvalidOperationException($"ID Column is invalid: '{idColumn}'.");
			}

			// Handle invalid ID Alias
			if (IsInvalidIdentifier(idAlias))
			{
				throw new InvalidOperationException($"ID Alias is invalid: '{idAlias}'.");
			}

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