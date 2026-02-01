// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient_Tests;

public abstract class PostgreSqlDbClient_Setup
{
	public (PostgreSqlDbClient client, Vars v) Setup()
	{
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var tableName = new TableName(schema, name);
		var table = Substitute.For<ITable>();
		table.GetName().Returns(tableName);

		var client = new PostgreSqlDbClient();

		return (client, new(table, schema, name));
	}

	public record class Vars(
		ITable Table,
		string Schema,
		string Name
	);
}
