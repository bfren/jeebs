// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Clients.SqlServer
{
	public partial class SqlServerDbClient : DbClient
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
				" SELECT SCOPE_IDENTITY();"
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
			var col = GetColumnsForRetrieveQuery(columns);

			// Build and return query
			return
				$"SELECT {JoinList(col, false)} " +
				$"FROM {Escape(table)} " +
				$"WHERE {Escape(idColumn)} = {id}"
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
			var col = GetColumnsForUpdateQuery(columns);

			// Add version column
			AddVersionToColumnList(col, versionColumn);

			// Begin query
			var sql = new StringBuilder(
				$"UPDATE {Escape(table)} " +
				$"SET {JoinList(col, false)} " +
				$"WHERE {Escape(idColumn)} = {id}"
			);

			// Add WHERE Version
			AddVersionToWhere(sql, versionColumn);

			// Return query
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
			AddVersionToWhere(sql, versionColumn);

			// Return query
			return sql.ToString();
		}
	}
}
