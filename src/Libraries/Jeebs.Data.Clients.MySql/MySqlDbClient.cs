// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Data;
using System.Text;
using Jeebs.Data.Enums;
using MySql.Data.MySqlClient;

namespace Jeebs.Data.Clients.MySql
{
	/// <inheritdoc cref="IDbClient"/>
	public class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		public override IDbConnection Connect(string connectionString) =>
			new MySqlConnection(connectionString);

		#region Custom Queries

		/// <inheritdoc/>
		protected override (string query, Dictionary<string, object> param) GetRetrieveQuery(
			string table,
			ColumnList columns,
			List<(string column, SearchOperator op, object value)> predicates
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}` AS '{column.Alias}'");
			}

			// Add each predicate to the where and parameter lists
			var where = new List<string>();
			var param = new Dictionary<string, object>();
			var index = 0;
			foreach (var (column, op, value) in predicates)
			{
				if (op == SearchOperator.In)
				{
					where.Add($"`{column}` {op.ToOperator()} ({value})");
				}
				else
				{
					var parameter = $"P{index++}";

					param.Add(parameter, value);
					where.Add($"`{column}` {op.ToOperator()} @{parameter}");
				}
			}

			// Return query and parameters
			return ($"SELECT {string.Join(", ", col)} FROM `{table}` WHERE {string.Join(" AND ", where)};", param);
		}

		#endregion

		#region CRUD Queries

		/// <inheritdoc/>
		protected override string GetCreateQuery(
			string table,
			IMappedColumnList columns
		)
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
		protected override string GetRetrieveQuery(
			string table,
			ColumnList columns,
			IMappedColumn idColumn,
			long id
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"`{column.Name}` AS '{column.Alias}'");
			}

			// Return query
			return $"SELECT {string.Join(", ", col)} FROM `{table}` WHERE `{idColumn.Name}` = {id};";
		}

		/// <inheritdoc/>
		protected override string GetUpdateQuery(
			string table,
			ColumnList columns,
			IMappedColumn idColumn,
			long id
		) =>
			GetUpdateQuery(table, columns, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetUpdateQuery(
			string table,
			ColumnList columns,
			IMappedColumn idColumn,
			long id,
			IMappedColumn? versionColumn
		)
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
			var sql = new StringBuilder($"UPDATE `{table}` SET {string.Join(", ", col)} WHERE `{idColumn.Name}` = {id}");

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
		protected override string GetDeleteQuery(
			string table,
			IMappedColumn idColumn,
			long id
		) =>
			GetDeleteQuery(table, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetDeleteQuery(
			string table,
			IMappedColumn idColumn,
			long id,
			IMappedColumn? versionColumn
		)
		{
			// Begin query
			var sql = new StringBuilder("DELETE FROM ");

			// Add table
			sql.Append($"`{table}` ");

			// Add WHERE id
			sql.Append($"WHERE `{idColumn.Name}` = {id}");

			// Add WHERE Version
			if (versionColumn is not null)
			{
				sql.Append($" AND `{versionColumn.Name}` = @{versionColumn.Alias}");
			}

			// Return query
			sql.Append(';');
			return sql.ToString();
		}

		#endregion
	}
}
