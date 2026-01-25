// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Data;
using Jeebs.Logging;
using Microsoft.Extensions.Options;
using Wrap;

namespace AppConsolePg;

internal sealed class Db : Jeebs.Data.Db
{
	public JsonTable Json { get; init; }

	public Db(IDbClient client, IOptions<DbConfig> config, ILog<Db> log) :
		base(client, config.Value.GetConnection("arwen").Unwrap(), log)
	{
		Json = new("console");

		Map<TestEntity>.To(Json);

		client.Types.AddIdTypeHandlers();
	}
}
