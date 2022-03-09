// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Id;

namespace Jeebs.Data.Map;

public record class FooWithVersion : Foo, IWithVersion
{
	[Version]
	public long Version { get; init; }
}
