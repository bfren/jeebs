// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.Db;
using Jeebs.Data.Common;
using Jeebs.Data.Map;
using Jeebs.Logging;
using Microsoft.Extensions.Options;

namespace AppConsolePg;

internal sealed class Db : Jeebs.Data.Common.Db
{
	public JsonTable Json { get; init; }

	public TestTable Test { get; init; }

	public Db(IDbClient client, IOptions<DbConfig> config, ILog<Db> log) :
		base(client, config.Value.GetConnection("arwen").Unwrap(), log)
	{
		var schema = "console";
		Json = new(schema);
		Test = new(schema);

		Map<JsonEntity>.To(Json);
		Map<TestObj>.To(Test);

		client.TypeMapper.AddIdTypeHandlers();
	}
}
