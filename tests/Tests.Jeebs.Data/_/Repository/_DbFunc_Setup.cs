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
		client
			.GetQuery<Foo, FooModel>(Arg.Any<(Expression<Func<Foo, object>>, Compare, object)[]>())
			.Returns((F.Rnd.Str, Substitute.For<IQueryParameters>()));
		client.GetCreateQuery<Foo>().Returns(F.Rnd.Str);
		client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
		client.GetUpdateQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
		client.GetDeleteQuery<Foo>(Arg.Any<long>()).Returns(F.Rnd.Str);

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

	public readonly record struct FooId(long Value) : IStrongId;
}
