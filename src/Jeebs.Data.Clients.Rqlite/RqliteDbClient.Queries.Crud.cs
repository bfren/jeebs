// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.Rqlite;

public partial class RqliteDbClient
{
	/// <inheritdoc/>
	protected override string GetCreateQuery(
		ITableName table,
		IColumnList columns
	)
	{
		// Get columns
		var (col, par) = GetColumnsForCreateQuery(columns);

		// Build and return query
		return
			$"INSERT INTO {Escape(table)} {JoinList(col, true)} " +
			$"VALUES {JoinList(par, true)}"
		;
	}

	/// <inheritdoc/>
	protected override string GetRetrieveQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn
	)
	{
		// Get columns
		var col = GetColumnsForRetrieveQuery(columns);

		// Build and return query
		return
			$"SELECT {JoinList(col, false)} " +
			$"FROM {Escape(table)} " +
			$"WHERE {Escape(idColumn)} = {GetParamRef(nameof(IWithId.Id))} " +
			$"LIMIT 1"
		;
	}

	/// <inheritdoc/>
	protected override string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn
	) =>
		GetUpdateQuery(table, columns, idColumn, null);

	/// <inheritdoc/>
	protected override string GetUpdateQuery(
		ITableName table,
		IColumnList columns,
		IColumn idColumn,
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
			$"WHERE {Escape(idColumn)} = {GetParamRef(nameof(IWithId.Id))}"
		;

		// Add WHERE Version
		sql = AddVersionToWhere(sql, versionColumn);

		// Return query
		return sql;
	}

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		ITableName table,
		IColumn idColumn
	) =>
		GetDeleteQuery(table, idColumn, null);

	/// <inheritdoc/>
	protected override string GetDeleteQuery(
		ITableName table,
		IColumn idColumn,
		IColumn? versionColumn
	)
	{
		// Begin query
		var sql =
			$"DELETE FROM {Escape(table)} " +
			$"WHERE {Escape(idColumn)} = {GetParamRef(nameof(IWithId.Id))}"
		;

		// Add WHERE Version
		sql = AddVersionToWhere(sql, versionColumn);

		// Return query
		return sql;
	}
}
