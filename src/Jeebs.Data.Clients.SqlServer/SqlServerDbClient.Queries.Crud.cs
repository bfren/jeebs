// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Clients.SqlServer;

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
		return new StringBuilder()
			.Append($"INSERT INTO {Escape(table)} {JoinList(col, true)} ")
			.Append($"VALUES {JoinList(par, true)};")
			.Append(" SELECT SCOPE_IDENTITY();")
			.ToString()
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
		return new StringBuilder()
			.Append($"SELECT {JoinList(col, false)} ")
			.Append($"FROM {Escape(table)} ")
			.Append($"WHERE {Escape(idColumn)} = {id}")
			.ToString()
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
		var sql = new StringBuilder()
			.Append($"UPDATE {Escape(table)} ")
			.Append($"SET {JoinList(set, false)} ")
			.Append($"WHERE {Escape(idColumn)} = {id}")
		;

		// Add WHERE Version
		AddVersionToWhere(sql, versionColumn);

		// Return query
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
		var sql = new StringBuilder()
			.Append($"DELETE FROM {Escape(table)} ")
			.Append($"WHERE {Escape(idColumn)} = {id}")
		;

		// Add WHERE Version
		AddVersionToWhere(sql, versionColumn);

		// Return query
		return sql.ToString();
	}
}
