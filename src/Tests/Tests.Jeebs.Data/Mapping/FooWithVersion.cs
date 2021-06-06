// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Mapping
{
	public record FooWithVersion : Foo, IEntityWithVersion
	{
		[Version]
		public long Version { get; init; }
	}
}
