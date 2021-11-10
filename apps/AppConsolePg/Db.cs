// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Jeebs.Config;
using Jeebs.Data;
using Microsoft.Extensions.Options;

namespace AppConsolePg;

internal class Db : Jeebs.Data.Db
{
	public JsonTable Json { get; init; }

	public Db(IDbClient client, IOptions<DbConfig> config, ILog<Db> log) : base(client, config.Value.GetConnection("server04"), log)
	{
		Json = new("console");

		Map<EntityTest>.To(Json);
	}

	static Db() =>
		AddTypeHandlers(mapper => mapper.AddStrongIdTypeHandlers());
}
