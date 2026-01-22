// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;

namespace Jeebs.Data.Testing.Query.FluentQueryHelper_Tests;

public abstract class Setup
{
	public static IFluentQuery<TestEntity, TestId> Create() =>
		FluentQueryHelper.CreateSubstitute<TestEntity, TestId>();

	public sealed record class TestId : LongId<TestId>;

	public sealed record class TestEntity(string Foo, long Bar, long? Nll) : WithId<TestId, long>;
}
