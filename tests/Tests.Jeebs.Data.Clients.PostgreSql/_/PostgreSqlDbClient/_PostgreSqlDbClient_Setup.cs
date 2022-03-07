// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient_Tests;

public static class PostgreSqlDbClient_Setup
{
	public static (PostgreSqlDbClient client, Vars v) Get()
	{
		var schema = F.Rnd.Str;
		var name = F.Rnd.Str;
		var tableName = new TableName(schema, name);
		var table = Substitute.For<ITable>();
		_ = table.GetName().Returns(tableName);
		var client = new PostgreSqlDbClient();

		return (client, new(table, schema, name));
	}

	public record class Vars(
		ITable Table,
		string Schema,
		string Name
	);
}
