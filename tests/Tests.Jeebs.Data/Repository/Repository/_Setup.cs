// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Data.Repository.Repository_Tests;

public abstract class Repository_Setup
{
	protected (Repository<Foo, FooId> repo, Vars v) Setup()
	{
		var client = Substitute.For<IDbClient>();
		client.GetCreateQuery<Foo>().Returns(Rnd.Str);
		client.GetRetrieveQuery<Foo, FooModel>().Returns(Rnd.Str);
		client.GetUpdateQuery<Foo, FooModel>().Returns(Rnd.Str);
		client.GetDeleteQuery<Foo>().Returns(Rnd.Str);

		var db = Substitute.For<IDb>();
		db.Client.Returns(client);

		var log = Substitute.For<ILog<Repository<Foo, FooId>>>();

		var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

		return (repo, new(db, client, log));
	}

	public sealed record class Vars(
		IDb Db,
		IDbClient Client,
		ILog Log
	);

	public sealed record class Foo : WithId<FooId, long>;

	public sealed record class FooModel : WithId<FooId, long>;

	public sealed record class FooId : LongId<FooId>;
}
