using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Clients.MySql
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		public long Version { get; set; }
	}
}
