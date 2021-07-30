// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;

namespace Jeebs.Data.Querying.QueryFluent_Tests
{
	public abstract class QueryFluent_Tests
	{
		public static (QueryFluent<TestEntity, TestId>, Vars) Setup()
		{
			var repo = Substitute.For<IRepository<TestEntity, TestId>>();

			var fluent = new QueryFluent<TestEntity, TestId>(repo);

			return (fluent, new(repo));
		}

		public sealed record Vars(
			IRepository<TestEntity, TestId> Repo
		);

		public sealed record TestId(ulong Value) : StrongId(Value)
		{
			public TestId() : this(0) { }
		}

		public sealed record TestEntity(TestId Id, string? Foo) : IWithId<TestId>;
	}
}
