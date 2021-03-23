// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;

namespace Jeebs.Data.DbFunc_Tests
{
	public static class DbFunc
	{
		public static (IDbClient client, DbFunc<Foo, FooId> crud) Get()
		{
			var client = Substitute.For<IDbClient>();
			client.GetCreateQuery<Foo>().Returns(F.Rnd.Str);
			client.GetRetrieveQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
			client.GetUpdateQuery<Foo, FooModel>(Arg.Any<long>()).Returns(F.Rnd.Str);
			client.GetDeleteQuery<Foo>(Arg.Any<long>()).Returns(F.Rnd.Str);

			var db = Substitute.For<IDb>();
			db.Client.Returns(client);

			var log = Substitute.For<ILog>();

			var crud = Substitute.For<DbFunc<Foo, FooId>>(db, log);

			return (client, crud);
		}

		public sealed record Foo : IEntity<FooId>
		{
			public FooId Id { get; init; } = new();
		}

		public sealed record FooModel : IWithId<FooId>
		{
			public FooId Id { get; init; } = new();
		}

		public sealed record FooId(long Value) : StrongId(Value)
		{
			public FooId() : this(0) { }
		}
	}
}
