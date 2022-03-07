// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using NSubstitute;

namespace Jeebs.Data.Repository_Tests;

public static class Repository_Setup
{
	public static (IDbClient client, ILog log, Repository<Foo, FooId> repo) Get()
	{
		var client = Substitute.For<IDbClient>();
		_ = client
			.GetQuery<Foo, FooModel>(Arg.Any<(Expression<Func<Foo, object>>, Compare, object)[]>())
			.Returns((F.Rnd.Str, Substitute.For<IQueryParametersDictionary>()));
		_ = client.GetCreateQuery<Foo>().Returns(F.Rnd.Str);
		_ = client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
		_ = client.GetUpdateQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
		_ = client.GetDeleteQuery<Foo>(Arg.Any<long>()).Returns(F.Rnd.Str);

		var db = Substitute.For<IDb>();
		_ = db.Client.Returns(client);

		var log = Substitute.For<ILog>();

		var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

		return (client, log, repo);
	}

	public sealed record class Foo : IWithId<FooId>
	{
		public FooId Id { get; init; }
	}

	public sealed record class FooModel : IWithId<FooId>
	{
		public FooId Id { get; init; }
	}

	public readonly record struct FooId(long Value) : IStrongId;
}
