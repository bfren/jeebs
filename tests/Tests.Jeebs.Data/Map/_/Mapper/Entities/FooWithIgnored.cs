// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using StrongId;

namespace Jeebs.Data.Map;

public record class FooWithIgnored : IWithId
{
	[Ignore]
	public IStrongId Id
	{
		get => FooId;
		init => FooId = value switch { FooId f => f, _ => new FooId() };
	}

	[Id]
	public FooId FooId { get; init; } = new();

	[Ignore]
	public string Bar0 { get; init; } = string.Empty;

	public string Bar1 { get; init; } = string.Empty;
}
