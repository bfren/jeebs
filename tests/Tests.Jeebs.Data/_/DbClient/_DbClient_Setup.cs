// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Config;
using NSubstitute;

namespace Jeebs.Data.DbClient_Tests;

public static class DbClient_Setup
{
	public static (DbConnectionConfig config, ILog log, IDbClient client, IDbConnection connection, Db db) Get()
	{
		var connectionString = F.Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var connection = Substitute.For<IDbConnection>();

		var client = Substitute.For<IDbClient>();
		client.Connect(Arg.Any<string>()).Returns(connection);

		var db = Substitute.ForPartsOf<Db>(client, config, log);

		return (config, log, client, connection, db);
	}
}
