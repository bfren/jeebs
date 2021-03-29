// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlDbClient : DbClient
	{
		/// <inheritdoc/>
		protected override string GetCreateQuery(
			string table,
			IMappedColumnList columns
		)
		{
			// Get columns
			var col = new List<string>();
			var par = new List<string>();
			foreach (var column in columns)
			{
				col.Add(Escape(column.Name));
				par.Add($"@{column.Alias}");
			}

			// Build and return query
			return
				$"INSERT INTO {Escape(table)} {JoinList(col, true)} " +
				$"VALUES {JoinList(par, true)};" +
				" SELECT LAST_INSERT_ID();"
			;
		}

		/// <inheritdoc/>
		protected override string GetRetrieveQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add(Escape(column, true));
			}

			// Build and return query
			return
				$"SELECT {JoinList(col, false)} " +
				$"FROM {Escape(table)} " +
				$"WHERE {Escape(idColumn)} = {id};"
			;
		}

		/// <inheritdoc/>
		protected override string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id
		) =>
			GetUpdateQuery(table, columns, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetUpdateQuery(
			string table,
			ColumnList columns,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		)
		{
			// Get columns
			var col = new List<string>();
			foreach (var column in columns)
			{
				col.Add($"{Escape(column)} = @{column.Alias}");
			}

			// Add version column
			if (versionColumn is not null)
			{
				col.Add($"{Escape(versionColumn)} = @{versionColumn.Alias} + 1");
			}

			// Begin query
			var sql = new StringBuilder(
				$"UPDATE {Escape(table)} " +
				$"SET {JoinList(col, false)} " +
				$"WHERE {Escape(idColumn)} = {id}"
			);

			// Add WHERE Version
			if (versionColumn is not null)
			{
				sql.Append($" AND {Escape(versionColumn)} = @{versionColumn.Alias}");
			}

			// Return query
			sql.Append(';');
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id
		) =>
			GetDeleteQuery(table, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetDeleteQuery(
			string table,
			IColumn idColumn,
			long id,
			IColumn? versionColumn
		)
		{
			// Begin query
			var sql = new StringBuilder(
				$"DELETE FROM {Escape(table)} " +
				$"WHERE {Escape(idColumn)} = {id}"
			);

			// Add WHERE Version
			if (versionColumn is not null)
			{
				sql.Append($" AND {Escape(versionColumn)} = @{versionColumn.Alias}");
			}

			// Return query
			sql.Append(';');
			return sql.ToString();
		}
	}
}
