// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Data;
using Jeebs.Data.Clients.Rqlite;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Rqlite.Client;

namespace AppConsoleRq;

internal sealed class Db : RqliteDb
{
	public Db(IRqliteClientFactory factory, IDbClient client, IOptions<DbConfig> config, ILog<Db> log) :
		base(factory, client, config, log, "local")
	{ }
}
