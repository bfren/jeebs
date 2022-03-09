// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Id;

namespace Jeebs.Data.Map.Mapper_Tests;

public class FooWithoutVersionAttribute : IWithVersion
{
	[Ignore]
	public IStrongId Id
	{
		get => FooId;
		init => FooId = new(value.Value);
	}

	[Id]
	public FooId FooId { get; init; } = new();

	public long Version { get; init; }

	public string Bar0 { get; init; } = string.Empty;

	public string Bar1 { get; init; } = string.Empty;
}
