using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		[Version]
		public long Version { get; set; }
	}
}
