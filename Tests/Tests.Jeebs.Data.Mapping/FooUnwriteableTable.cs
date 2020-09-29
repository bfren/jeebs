using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class FooUnwriteableTable : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar2 = "foo_bar2";

		public readonly string Bar3 = "foo_bar3";
		
		public FooUnwriteableTable() : base("foo_unwriteable") { }
	}
}
