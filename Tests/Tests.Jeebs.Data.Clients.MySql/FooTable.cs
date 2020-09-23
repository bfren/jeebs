using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public class FooTable : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar0 = "foo_bar0";

		public readonly string Bar1 = "foo_bar1";

		public FooTable() : base("foo") { }
	}
}
