// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Config.Db;
using Jeebs.Logging;

namespace Jeebs.Data.Common.DbClient_Tests;

public static class DbClient_Setup
{
	public static (DbConnectionConfig config, ILog log, IDbClient client, IDbConnection connection, Db db) Get()
	{
		var connectionString = Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var connection = Substitute.For<IDbConnection>();

		var client = Substitute.For<IDbClient>();
		client.GetConnection(Arg.Any<string>()).Returns(connection);

		var db = Substitute.ForPartsOf<Db>(client, config, log);

		return (config, log, client, connection, db);
	}
}
