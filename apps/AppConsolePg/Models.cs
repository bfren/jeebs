// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Wrap;

namespace AppConsolePg;

public record struct TestObj(int Id, string Foo, string Bar);

public sealed record class TestId : LongId<TestId>;

public sealed record class TestEntity : WithId<TestId, long>
{
	public TestObj Value { get; init; }
}