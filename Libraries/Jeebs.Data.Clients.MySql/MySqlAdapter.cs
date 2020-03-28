using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jeebs.Data.Clients.MySql
{
	/// <summary>
	/// MySql adapter
	/// </summary>
	public sealed class MySqlAdapter : Adapter
	{
		/// <summary>
		/// Create object
		/// </summary>
		public MySqlAdapter() : base('.', '`', '`', "AS", '\'', '\'', "ASC", "DESC") { }

		/// <summary>
		/// Return random sort string
		/// </summary>
		public override string GetRandomSortOrder() => "RAND()";

		/// <summary>
		/// Query to insert a single row and return the new ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string CreateSingleAndReturnId<T>()
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Return SQL
			return $"INSERT INTO {map.Name} ({string.Join(", ", columns)}) " +
				$"VALUES (@{string.Join(", @", aliases)}); " +
				$"SELECT LAST_INSERT_ID();";
		}

		/// <summary>
		/// Query to retrieve a single row by ID
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string RetrieveSingleById<T>()
		{
			// Get map and select list
			var map = TableMaps.GetMap<T>();
			var select = new List<string>();

			// Add each column to the select list
			foreach (var mc in map.Columns)
			{
				select.Add(GetColumn(mc));
			}

			// Return SQL
			return $"SELECT {string.Join(", ", select)} FROM {map} WHERE {map.IdColumn} = @Id;";
		}

		/// <summary>
		/// Build a SELECT query
		/// </summary>
		/// <param name="args">QueryArgs</param>
		/// <returns>SELECT query</returns>
		public override string Retrieve<T>(QueryArgs<T> args)
		{
			// Start query
			StringBuilder sql = new StringBuilder($"SELECT {args.Select ?? "*"} FROM {args.From}");

			// Add INNER JOIN
			if (args.InnerJoin is List<(string table, string on, string equals)> innerJoinValues)
			{
				foreach (var item in innerJoinValues)
				{
					sql.Append($" INNER JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add LEFT JOIN
			if (args.LeftJoin is List<(string table, string on, string equals)> leftJoinValues)
			{
				foreach (var item in leftJoinValues)
				{
					sql.Append($" LEFT JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add RIGHT JOIN
			if (args.RightJoin is List<(string table, string on, string equals)> rightJoinValues)
			{
				foreach (var item in rightJoinValues)
				{
					sql.Append($" RIGHT JOIN {item.table} ON {item.on} = {item.equals}");
				}
			}

			// Add WHERE
			if (args.Where is List<string> whereValue)
			{
				sql.Append($" WHERE {string.Join(" AND ", whereValue)}");
			}

			// Add ORDER BY
			if (args.OrderBy is List<string> orderByValue)
			{
				sql.Append($" ORDER BY {string.Join(", ", orderByValue)}");
			}

			// Add LIMIT
			if (args.Limit is long limitValue && limitValue > 0)
			{
				sql.Append($" LIMIT {limitValue}");
			}

			// Add OFFSET
			if (args.Offset is long offsetValue && offsetValue > 0)
			{
				sql.Append($" OFFSET {offsetValue}");
			}

			// Append semi-colon
			sql.Append(";");

			// Return query string
			return sql.ToString();
		}

		/// <summary>
		/// Query to update a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string UpdateSingle<T>()
		{
			// Get map and columns
			var map = TableMaps.GetMap<T>();
			(var columns, var aliases) = map.GetWriteableColumnsAndAliases();

			// Add each column to the update list
			var update = new List<string>();
			for (int i = 0; i < columns.Count; i++)
			{
				update.Add($"{columns[i]} = @{aliases[i]}");
			}

			// Build SQL
			var sql = $"UPDATE {map} " +
				$"SET {string.Join(", ", update)} " +
				$"WHERE {map.IdColumn} = @{map.IdColumn.Property.Name}";

			// Add Version column
			if (map.VersionColumn is MappedColumn versionColumn)
			{
				sql += $" AND {versionColumn} = @{versionColumn.Property.Name} - 1";
			}

			// Return SQL
			return $"{sql};";
		}

		/// <summary>
		/// Query to delete a single row
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		public override string DeleteSingle<T>()
		{
			// Get map
			var map = TableMaps.GetMap<T>();

			// Build SQL
			var sql = $"DELETE FROM {map} WHERE {map.IdColumn} = @{map.IdColumn.Property.Name}";

			// Add Version column
			if (map.VersionColumn is MappedColumn versionColumn)
			{
				sql += $" AND {versionColumn} = @{versionColumn.Property.Name}";
			}

			// Return SQL
			return $"{sql};";
		}
	}
}