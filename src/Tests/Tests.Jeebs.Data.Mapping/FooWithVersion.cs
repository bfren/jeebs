// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Mapping
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		[Version]
		public long Version { get; set; }
	}
}
