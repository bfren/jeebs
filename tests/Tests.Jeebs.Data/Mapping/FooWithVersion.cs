// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping;

public record class FooWithVersion : Foo, IWithVersion
{
	[Version]
	public long Version { get; init; }
}
