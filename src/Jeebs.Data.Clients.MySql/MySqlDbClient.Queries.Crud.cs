// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Text;
using Jeebs.Data.Mapping;

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
			var (col, par) = GetColumnsForCreateQuery(columns);

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
			IColumnList columns,
			IColumn idColumn,
			ulong id
		)
		{
			// Get columns
			var col = GetColumnsForRetrieveQuery(columns);

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
			IColumnList columns,
			IColumn idColumn,
			ulong id
		) =>
			GetUpdateQuery(table, columns, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetUpdateQuery(
			string table,
			IColumnList columns,
			IColumn idColumn,
			ulong id,
			IColumn? versionColumn
		)
		{
			// Get set list
			var set = GetSetListForUpdateQuery(columns);

			// Add version
			AddVersionToSetList(set, versionColumn);

			// Begin query
			var sql = new StringBuilder(
				$"UPDATE {Escape(table)} " +
				$"SET {JoinList(set, false)} " +
				$"WHERE {Escape(idColumn)} = {id}"
			);

			// Add WHERE Version
			AddVersionToWhere(sql, versionColumn);

			// Return query
			sql.Append(';');
			return sql.ToString();
		}

		/// <inheritdoc/>
		protected override string GetDeleteQuery(
			string table,
			IColumn idColumn,
			ulong id
		) =>
			GetDeleteQuery(table, idColumn, id, null);

		/// <inheritdoc/>
		protected override string GetDeleteQuery(
			string table,
			IColumn idColumn,
			ulong id,
			IColumn? versionColumn
		)
		{
			// Begin query
			var sql = new StringBuilder(
				$"DELETE FROM {Escape(table)} " +
				$"WHERE {Escape(idColumn)} = {id}"
			);

			// Add WHERE Version
			AddVersionToWhere(sql, versionColumn);

			// Return query
			sql.Append(';');
			return sql.ToString();
		}
	}
}
