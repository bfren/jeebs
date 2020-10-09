using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class FooWithVersionTable : Table
	{
		public readonly string Id = "foo_with_version_id";

		public readonly string Bar0 = "foo_with_version_bar0";

		public readonly string Bar1 = "foo_with_version_bar1";

		public readonly string Version = "foo_with_version_version";

		public FooWithVersionTable() : base("foo_with_version") { }
	}
}
