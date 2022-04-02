// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.Logging;
using StrongId;

namespace Jeebs.Data.Repository_Tests;

public static class Repository_Setup
{
	public static (IDbClient client, ILog log, Repository<Foo, FooId> repo) Get()
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

		var log = Substitute.For<ILog>();

		var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

		return (client, log, repo);
	}

	public sealed record class Foo : IWithId<FooId>
	{
		public FooId Id { get; init; } = new();
	}

	public sealed record class FooModel : IWithId<FooId>
	{
		public FooId Id { get; init; } = new();
	}

	public sealed record class FooId : LongId;
}
