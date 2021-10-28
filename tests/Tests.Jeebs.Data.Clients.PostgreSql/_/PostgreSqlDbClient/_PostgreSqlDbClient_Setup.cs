// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Clients.PostgreSql.MySqlDbClient_Tests;

public static class PostgreSqlDbClient_Setup
{
	public static (PostgreSqlDbClient client, ITable table) Get()
	{
		var tableName = F.Rnd.Str;
		var table = Substitute.For<ITable>();
		table.GetName().Returns(tableName);
		var client = new PostgreSqlDbClient();

		return (client, table);
	}
}
