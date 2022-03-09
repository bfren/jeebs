// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Id;

namespace Jeebs.Data.Map;

public class FooUnwriteable : IWithId
{
	[Ignore]
	public IStrongId Id
	{
		get => FooId;
		init => FooId = new(value.Value);
	}

	[Id]
	public FooId FooId { get; init; } = new();

	[Computed]
	public string Bar2 { get; init; } = string.Empty;

	[Readonly]
	public string Bar3 { get; init; } = string.Empty;
}
