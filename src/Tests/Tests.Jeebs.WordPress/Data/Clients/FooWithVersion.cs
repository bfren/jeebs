// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Clients.MySql
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		public long Version { get; set; }
	}
}
