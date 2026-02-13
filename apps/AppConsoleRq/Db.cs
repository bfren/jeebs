// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Data;
using Jeebs.Data.Clients.Rqlite;
using Jeebs.Data.Map;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Rqlite.Client;
using Wrap.Json;

namespace AppConsoleRq;

internal sealed class Db : RqliteDb
{
	public TestTable Test { get; init; }

	public Db(IRqliteClientFactory factory, IDbClient client, IOptions<DbConfig> config, ILog<Db> log) :
		base(factory, client, config, log, "local")
	{
		factory.JsonOptions.Converters.AddWrapConverters();

		Test = new();

		Map<TestEntity>.To(Test);
	}
}
