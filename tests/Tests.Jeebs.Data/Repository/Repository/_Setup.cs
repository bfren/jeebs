// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.Logging;

namespace Jeebs.Data.Repository.Repository_Tests;

public abstract class Repository_Setup
{
	protected (Repository<Foo, FooId> repo, Vars v) Setup()
	{
		var client = Substitute.For<IDbClient>();
		client
			.GetQuery<Foo, FooModel>(Arg.Any<(string, Compare, dynamic)[]>())
			.Returns((Rnd.Str, Substitute.For<IQueryParametersDictionary>()));
		client.GetCreateQuery<Foo>().Returns(Rnd.Str);
		client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<long>()).Returns(Rnd.Str);
		client.GetUpdateQuery<Foo, FooModel>(Arg.Any<long>()).Returns(Rnd.Str);
		client.GetDeleteQuery<Foo>(Arg.Any<long>()).Returns(Rnd.Str);

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
