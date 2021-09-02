﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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

		public sealed record class Vars(
			IRepository<TestEntity, TestId> Repo
		);

		public readonly record struct TestId(ulong Value) : IStrongId;

		public sealed record class TestEntity(TestId Id, string? Foo) : IWithId<TestId>;
	}
}
