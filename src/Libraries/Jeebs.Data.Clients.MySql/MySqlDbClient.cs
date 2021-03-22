// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbClient"/>
	public class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		public override IDbConnection Connect(string connectionString) =>
			new MySqlConnection(connectionString);

		/// <inheritdoc/>
		protected override string GetCreateQuery(string table, IMappedColumnList columns)
		{
			// Begin query
			var sql = new StringBuilder($"INSERT INTO `{table}` ");

			// Get columns
			var col = new List<string>();
			var par = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}`");
				par.Add($"@{column.Alias}");
			}

			// Add columns and parameters
			sql.Append($"({string.Join(", ", col)}) VALUES ({string.Join(", ", par)}); ");

			// Select ID
			sql.Append("SELECT LAST_INSERT_ID();");

			// Return query
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetRetrieveQuery(string table, ColumnList columns, IMappedColumn idColumn)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}` AS '{column.Alias}'");
			}

			// Return query
			return $"SELECT {string.Join(", ", col)} FROM `{table}` WHERE `{idColumn.Name}` = @{idColumn.Alias};";
		}

		/// <inheritdoc/>
		protected override string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn) =>
			GetUpdateQuery(table, columns, idColumn, null);

		/// <inheritdoc/>
		protected override string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn, IMappedColumn? versionColumn)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}` = @{column.Alias}");
			}

			// Add version column
			if (versionColumn is not null)
			{
				col.Add($"`{versionColumn.Name}` = @{versionColumn.Alias} + 1");
			}

			// Add WHERE Id
			var sql = new StringBuilder($"UPDATE `{table}` SET {string.Join(", ", col)} WHERE `{idColumn.Name}` = @{idColumn.Alias}");

			// Add WHERE Version
			if (versionColumn is not null)
			{
				sql.Append($" AND `{versionColumn.Name}` = @{versionColumn.Alias}");
			}

			// Return query
			sql.Append(';');
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetDeleteQuery(string table, IMappedColumn idColumn) =>
			GetDeleteQuery(table, idColumn, null);

		/// <inheritdoc/>
		protected override string GetDeleteQuery(string table, IMappedColumn idColumn, IMappedColumn? versionColumn)
		{
			// Begin query
			var sql = new StringBuilder("DELETE FROM ");

			// Add table
			sql.Append($"`{table}` ");

			// Add WHERE id
			sql.Append($"WHERE `{idColumn.Name}` = @{idColumn.Alias}");

			// Add WHERE Version
			if (versionColumn is not null)
			{
				sql.Append($" AND `{versionColumn.Name}` = @{versionColumn.Alias}");
			}

			// Return query
			sql.Append(';');
			return sql.ToString();
		}
	}
}
