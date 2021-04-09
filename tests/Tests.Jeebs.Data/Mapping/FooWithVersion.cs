// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping
{
	public record FooWithVersion : Foo, IWithVersion
	{
		[Version]
		public long Version { get; init; }
	}
}
