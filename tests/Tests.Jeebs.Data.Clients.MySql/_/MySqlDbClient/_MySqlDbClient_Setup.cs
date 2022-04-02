// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public static class MySqlDbClient_Setup
{
	public static (MySqlDbClient client, Vars v) Get()
	{
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var tableName = new TableName(schema, name);
		var table = Substitute.For<ITable>();
		table.GetName().Returns(tableName);
		var client = new MySqlDbClient();

		return (client, new(table, schema, name));
	}

	public record class Vars(
		ITable Table,
		string Schema,
		string Name
	);
}
