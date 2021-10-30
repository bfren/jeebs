﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Npgsql;

namespace Jeebs.Data.Clients.PostgreSql;

/// <inheritdoc cref="IDbClient"/>
public partial class PostgreSqlDbClient : DbClient
{
	/// <inheritdoc/>
	public override IDbConnection Connect(string connectionString) =>
		new NpgsqlConnection(connectionString);

	/// <inheritdoc/>
	public override string Escape(IColumn column, bool withAlias = false) =>
		withAlias switch
		{
			true =>
				Escape(column.Name) + $" AS {Escape(column.Alias)}",

			false =>
				Escape(column.Name)
		};

	/// <inheritdoc/>
	public override string EscapeWithTable(IColumn column, bool withAlias = false) =>
		withAlias switch
		{
			true =>
				Escape(column.Name, column.Table) + $" AS {Escape(column.Alias)}",

			false =>
				Escape(column.Name, column.Table)
		};

	/// <inheritdoc/>
	public override string Escape(ITable table) =>
		Escape(table.GetName());

	/// <inheritdoc/>
	public override string Escape(string columnOrTable)
	{
		var elements = columnOrTable.Split('.');
		return @"""" + string.Join(@""".""", elements) + @"""";
	}

	/// <inheritdoc/>
	public override string Escape(string column, string table) =>
		Escape($"{table}.{column}");

	/// <inheritdoc/>
	public override string GetOperator(Compare cmp) =>
		cmp.ToOperator();

	/// <inheritdoc/>
	public override string GetParamRef(string paramName) =>
		$"@{paramName}";

	/// <inheritdoc/>
	public override string JoinList(List<string> objects, bool wrap)
	{
		var list = string.Join(", ", objects);
		return wrap switch
		{
			true =>
				$"({list})",

			false =>
				list
		};
	}
}
