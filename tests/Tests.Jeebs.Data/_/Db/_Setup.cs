// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Functions;
using Jeebs.Logging;

namespace Jeebs.Data.Db_Tests;

public abstract class Db_Setup
{
	public static (Db, Vars) Setup()
	{
		var connectionString = Rnd.Str;
		var config = new DbConnectionConfig { ConnectionString = connectionString };

		var log = Substitute.For<ILog>();

		var client = Substitute.For<IDbClient>();

		var db = Substitute.ForPartsOf<Db>(client, config, log);

		return (db, new(client, config, log));
	}

	public sealed record Vars(
		IDbClient Client,
		DbConnectionConfig Config,
		ILog Log
	);

	public static bool Cmp(object expected, object actual) =>
		Equals(
			JsonF.Serialise(expected).Unsafe().Unwrap(),
			JsonF.Serialise(actual).Unsafe().Unwrap()
		);
}
