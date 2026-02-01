// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Wrap;
using Wrap.Ids;

namespace AppConsolePg;

public sealed record class TestObj(string Foo, string Bar, ulong Fred) : WithId<TestId, long>
{
	public TestObj() : this(string.Empty, string.Empty, 0) { }

	public TestObj(long id, string foo, string bar, ulong fred) : this(foo, bar, fred) =>
		Id = TestId.Wrap(id);
}

public sealed record class TestId : LongId<TestId>;

public sealed record class JsonEntity : WithId<TestId, long>
{
	public TestObj Value { get; init; } = new();
}
