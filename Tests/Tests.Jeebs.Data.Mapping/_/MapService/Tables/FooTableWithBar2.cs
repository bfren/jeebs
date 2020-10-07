using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooTableWithBar2 : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar0 = "foo_bar0";

		public readonly string Bar1 = "foo_bar1";

		public readonly string Bar2 = "foo_bar2";

		public FooTableWithBar2() : base("foo_with_bar2") { }
	}
}
