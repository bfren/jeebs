// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Config.WordPress;
using Jeebs.Data.Common;

namespace Jeebs.WordPress.Functions;

public abstract class Query_Tests
{
	public static (IWpDb, IUnitOfWork, Vars) Setup()
	{
		var schema = new WpDbSchema(Rnd.Str);

		var uploadsPath = Rnd.Str;
		var config = new WpConfig { UploadsPath = uploadsPath };

		var db = Substitute.For<IWpDb>();
		db.Schema.Returns(schema);
		db.WpConfig.Returns(config);

		var unitOfWork = Substitute.For<IUnitOfWork>();

		return (db, unitOfWork, new(schema, config, uploadsPath));
	}

	public sealed record class Vars(
		WpDbSchema Schema,
		WpConfig Config,
		string UploadsPath
	);
}
