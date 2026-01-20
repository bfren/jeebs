// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map;

public record class Foo : IWithId
{
	[Ignore]
	public IUnion Id
	{
		get => FooId;
		init => FooId = value switch { FooId f => f, _ => new() };
	}

	public FooId FooId { get; init; } = new();

	public string Bar0 { get; init; } = string.Empty;

	public string Bar1 { get; init; } = string.Empty;
}
