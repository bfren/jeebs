// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbClient"/>
	public sealed class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		public override IDbConnection Connect(string connectionString) =>
			new MySqlConnection(connectionString);

		/// <inheritdoc/>
		protected override string GetCreateQuery(string table, IMappedColumnList columns)
		{
			// Begin query
			var sql = new StringBuilder("INSERT INTO ");

			// Add table
			sql.Append($"`{table}` ");

			// Get columns
			var col = new List<string>();
			var par = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}`");
				par.Add($"@{column.Alias}");
			}

			// Add columns and parameters
			sql.Append($" ({string.Join(", ", col)}) VALUES ({string.Join(", ", par)}); ");

			// Select ID
			sql.Append("SELECT LAST_INSERT_ID();");

			// Return query
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetRetrieveQuery(string table, ColumnList columns, IMappedColumn idColumn)
		{
			// Begin query
			var sql = new StringBuilder("SELECT ");

			// Add columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}` AS '{column.Alias}'");
			}

			sql.Append(string.Join(", ", col));

			// Add WHERE id
			sql.Append($" WHERE `{idColumn.Name}` = @{idColumn.Alias};");

			// Return query
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn) =>
			GetUpdateQuery(table, columns, idColumn, null);

		/// <inheritdoc/>
		protected override string GetUpdateQuery(string table, ColumnList columns, IMappedColumn idColumn, IMappedColumn? versionColumn)
		{
			// Begin query
			var sql = new StringBuilder("UPDATE ");

			// Add table
			sql.Append($"`{table}` ");

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

			// Add update columns
			sql.Append($"SET {string.Join(", ", col)} ");

			// Add WHERE Id
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

		/// <inheritdoc/>
		protected override string GetDeleteQuery(string table, IMappedColumn idColumn)
		{
			// Begin query
			var sql = new StringBuilder("DELETE FROM ");

			// Add table
			sql.Append($"`{table}` ");

			// Add WHERE id
			sql.Append($"WHERE `{idColumn.Name}` = @{idColumn.Alias};");

			// Return query
			return sql.ToString();
		}
	}
}
