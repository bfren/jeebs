// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using NSubstitute;

namespace Jeebs.Data.Repository_Tests
{
	public static class Repository_Setup
	{
		public static (IDbClient client, ILog log, Repository<Foo, FooId> repo) Get()
		{
			var client = Substitute.For<IDbClient>();
			client
				.GetQuery<Foo, FooModel>(Arg.Any<(Expression<Func<Foo, object>>, Compare, object)[]>())
				.Returns((F.Rnd.Str, Substitute.For<IQueryParameters>()));
			client.GetCreateQuery<Foo>().Returns(F.Rnd.Str);
			client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<ulong>()).Returns(F.Rnd.Str);
			client.GetUpdateQuery<Foo, FooModel>(Arg.Any<ulong>()).Returns(F.Rnd.Str);
			client.GetDeleteQuery<Foo>(Arg.Any<ulong>()).Returns(F.Rnd.Str);

			var db = Substitute.For<IDb>();
			db.Client.Returns(client);

			var log = Substitute.For<ILog>();

			var repo = Substitute.ForPartsOf<Repository<Foo, FooId>>(db, log);

			return (client, log, repo);
		}

		public sealed record Foo : IWithId<FooId>
		{
			public FooId Id { get; init; } = new();
		}

		public sealed record FooModel : IWithId<FooId>
		{
			public FooId Id { get; init; } = new();
		}

		public sealed record FooId(ulong Value) : StrongId(Value)
		{
			public FooId() : this(0) { }
		}
	}
}
