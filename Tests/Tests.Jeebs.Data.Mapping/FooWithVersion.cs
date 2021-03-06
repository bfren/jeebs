using System;

namespace Jeebs.Data.Mapping
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		[Version]
		public long Version { get; set; }
	}
}
