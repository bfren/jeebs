// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Config.WordPress;
using Jeebs.Data;

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

		var transaction = Substitute.For<IDbTransaction>();

		var unitOfWork = Substitute.For<IUnitOfWork>();
		unitOfWork.Transaction.Returns(transaction);

		return (db, unitOfWork, new(schema, config, uploadsPath, transaction));
	}

	public sealed record class Vars(
		WpDbSchema Schema,
		WpConfig Config,
		string UploadsPath,
		IDbTransaction Transaction
	);
}
