// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using Jeebs.Config;
using NSubstitute;

namespace Jeebs.Data.Db_Tests
{
	public static class Db_Setup
	{
		public static (DbConnectionConfig config, ILog log, IDbClient client, IDbConnection connection, Db db) Get()
		{
			var connectionString = F.Rnd.Str;
			var config = new DbConnectionConfig { ConnectionString = connectionString };

			var log = Substitute.For<ILog>();

			var connection = Substitute.For<IDbConnection>();

			var client = Substitute.For<IDbClient>();
			client.Connect(Arg.Any<string>()).Returns(connection);

			var name = F.Rnd.Str;

			var db = Substitute.ForPartsOf<Db>(config, log, client, name);

			return (config, log, client, connection, db);
		}
	}
}
