using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public class FooWithVersion : Foo, IEntityWithVersion
	{
		[Version]
		public long Version { get; set; }
	}
}
