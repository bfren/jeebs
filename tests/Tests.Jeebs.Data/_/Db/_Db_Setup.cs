// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Data.Common;
using Jeebs.Config.Db;
using Jeebs.Logging;
using NSubstitute.Extensions;

namespace Jeebs.Data.Db_Tests;

public static class Db_Setup
{
	public static (Db, Vars) Get()
	{
		var connectionString = Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var connection = Substitute.ForPartsOf<DbConnection>();

		var client = Substitute.For<IDbClient>();
		client.GetConnection(Arg.Any<string>()).Returns(connection);

		var transaction = Substitute.ForPartsOf<DbTransaction>();

		var w = Substitute.For<IUnitOfWork>();
		w.Connection.Returns(connection);
		w.Transaction.Returns(transaction);

		var db = Substitute.ForPartsOf<Db>(client, config, log);
		db.Configure().StartWork().Returns(w);
		db.Configure().StartWorkAsync().Returns(Task.FromResult(w));

		return (db, new(client, config, connection, log, transaction));
	}

	public sealed record Vars(
		IDbClient Client,
		DbConnectionConfig Config,
		IDbConnection Connection,
		ILog Log,
		IDbTransaction Transaction
	);
}
