// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using StrongId;

namespace Jeebs.Data.Map.Mapper_Tests;

public class FooWithMultipleVersionAttributes : IWithVersion
{
	[Ignore]
	public IStrongId Id
	{
		get => FooId;
		init => FooId = value switch { FooId f => f, _ => new FooId() };
	}

	[Id]
	public FooId FooId { get; init; } = new();

	[Version]
	public long Version { get; init; }

	[Version]
	public string Bar0 { get; init; } = string.Empty;

	[Version]
	public string Bar1 { get; init; } = string.Empty;
}
