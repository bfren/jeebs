// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;

namespace Jeebs.Data.Clients.Sqlite;

public partial class SqliteDbClient : DbClient
{
	/// <inheritdoc/>
	protected override string GetCreateQuery(
		IDbName table,
		IColumnList columns
	)
	{
		// Get columns
		var (col, par) = GetColumnsForCreateQuery(columns);

		// Build and return query
		return
			$"INSERT INTO {Escape(table)} {JoinList(col, true)} " +
			$"VALUES {JoinList(par, true)};" +
			" SELECT last_insert_rowid();"
		;
	}

	/// <inheritdoc/>
	protected override string GetRetrieveQuery(
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id
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
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	) =>
		GetUpdateQuery(table, columns, idColumn, id, null);

	/// <inheritdoc/>
	protected override string GetUpdateQuery(
		IDbName table,
		IColumnList columns,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	)
	{
		// Get set list
		var set = GetSetListForUpdateQuery(columns);

		// Add version
		AddVersionToSetList(set, versionColumn);

		// Begin query
		var sql =
			$"UPDATE {Escape(table)} " +
			$"SET {JoinList(set, false)} " +
			$"WHERE {Escape(idColumn)} = {id}"
		;

		// Add WHERE Version
		sql = AddVersionToWhere(sql, versionColumn);

		// Return query
		return $"{sql};";
	}

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		IDbName table,
		IColumn idColumn,
		object id
	) =>
		GetDeleteQuery(table, idColumn, id, null);

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		IDbName table,
		IColumn idColumn,
		object id,
		IColumn? versionColumn
	)
	{
		// Begin query
		var sql =
			$"DELETE FROM {Escape(table)} " +
			$"WHERE {Escape(idColumn)} = {id}"
		;

		// Add WHERE Version
		sql = AddVersionToWhere(sql, versionColumn);

		// Return query
		return $"{sql};";
	}
}
