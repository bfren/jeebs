// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.SqlServer;

public partial class SqlServerDbClient : DbClient
{
	/// <inheritdoc/>
	protected override string GetCreateQuery(
		ITableName table,
		IMappedColumnList columns
	)
	{
		// Get columns
		var (col, par) = GetColumnsForCreateQuery(columns);

		// Build and return query
		return
			$"INSERT INTO {Escape(table)} {JoinList(col, true)} " +
			$"VALUES {JoinList(par, true)}; " +
			"SELECT SCOPE_IDENTITY();"
		;
	}

	/// <inheritdoc/>
	protected override string GetRetrieveQuery(
		ITableName table,
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
			$"WHERE {Escape(idColumn)} = {id}"
		;
	}

	/// <inheritdoc/>
	protected override string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
		object id
	) =>
		GetUpdateQuery(table, columns, idColumn, id, null);

	/// <inheritdoc/>
	protected override string GetUpdateQuery(
		ITableName table,
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
		return AddVersionToWhere(sql, versionColumn);
	}

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		ITableName table,
		IColumn idColumn,
		object id
	) =>
		GetDeleteQuery(table, idColumn, id, null);

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		ITableName table,
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
		return AddVersionToWhere(sql, versionColumn);
	}
}
