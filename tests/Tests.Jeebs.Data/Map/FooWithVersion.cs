// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map;

public record class FooWithVersion : Foo, IWithVersion
{
	public long Version { get; init; }
}
