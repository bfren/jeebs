using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class FooUnwriteableTable : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar0 = "foo_bar0";

		public readonly string Bar1 = "foo_bar1";
		
		public FooUnwriteableTable() : base("foo") { }
	}
}
