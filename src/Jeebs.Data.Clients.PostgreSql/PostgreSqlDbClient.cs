// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Npgsql;

namespace Jeebs.Data.Clients.PostgreSql;

/// <inheritdoc cref="IDbClient"/>
public partial class PostgreSqlDbClient : DbClient
{
	/// <inheritdoc/>
	public override IDbConnection Connect(string connectionString) =>
		new NpgsqlConnection(connectionString);

	/// <inheritdoc/>
	public override string Escape(IDbName table) =>
		table.GetFullName(Escape);

	/// <inheritdoc/>
	public override string Escape(IDbName table, string column) =>
		Escape(table) + "." + Escape(column);

	/// <inheritdoc/>
	public override string Escape(IColumn column, bool withAlias) =>
		withAlias switch
		{
			true =>
				Escape(column.ColName) + " AS " + Escape(column.ColAlias),

			false =>
				Escape(column.ColName)
		};

	/// <inheritdoc/>
	public override string EscapeWithTable(IColumn column, bool withAlias) =>
		withAlias switch
		{
			true =>
				Escape(column.TblName, column.ColName) + " AS " + Escape(column.ColAlias),

			false =>
				Escape(column.TblName, column.ColName)
		};

	/// <inheritdoc/>
	public override string Escape(string obj) =>
		@$"""{obj}""";

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
