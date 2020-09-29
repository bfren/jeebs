using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.Map_Tests
{
	public class FooTableWithoutBar0 : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar1 = "foo_bar1";

		public FooTableWithoutBar0() : base("foo") { }
	}
}
