// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json.Serialization;
using Wrap;
using Wrap.Ids;

namespace AppConsoleRq;

public record class TestObj : TestEntity
{
	[JsonPropertyName("freddie")]
	public new ulong Fred { get; init; }
}

public record class TestEntity() : WithId<TestId, long>
{
	public string Foo { get; init; } = string.Empty;

	public string Bar { get; init; } = string.Empty;

	public ulong Fred { get; init; }

	public TestEntity(long id, string foo, string bar, ulong fred) : this() =>
		(Id, Foo, Bar, Fred) = (TestId.Wrap(id), foo, bar, fred);
}

public sealed record class TestId : LongId<TestId>;
