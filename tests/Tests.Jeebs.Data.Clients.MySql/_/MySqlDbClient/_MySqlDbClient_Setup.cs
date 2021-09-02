﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public static class MySqlDbClient_Setup
{
	public static (MySqlDbClient client, ITable table) Get()
	{
		var tableName = F.Rnd.Str;
		var table = Substitute.For<ITable>();
		table.GetName().Returns(tableName);
		var client = new MySqlDbClient();

		return (client, table);
	}
}
