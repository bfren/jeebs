// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping
{
	public record FooWithVersion : Foo, IWithVersion
	{
		[Version]
		public ulong Version { get; init; }
	}
}
