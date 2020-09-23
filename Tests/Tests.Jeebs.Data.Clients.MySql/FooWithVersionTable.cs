using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public class FooWithVersionTable : Table
	{
		public readonly string Id = "foo_id";

		public readonly string Bar0 = "foo_bar0";

		public readonly string Bar1 = "foo_bar1";

		public readonly string Version = "foo_version";

		public FooWithVersionTable() : base("foo_with_version") { }
	}
}
