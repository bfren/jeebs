using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		private class Foo : IEntity
		{
			[Id]
			public long Id { get; set; }

			public string Bar0 { get; set; } = string.Empty;

			public string Bar1 { get; set; } = string.Empty;
		}

		private class FooWithVersion : Foo, IEntityWithVersion
		{
			[Version]
			public long Version { get; set; }
		}

		private class FooTable : Table
		{
			public readonly string Id = "foo_id";

			public readonly string Bar0 = "foo_bar0";

			public readonly string Bar1 = "foo_bar1";

			public FooTable() : base("foo") { }
		}

		private class FooWithVersionTable : Table
		{
			public readonly string Id = "foo_id";

			public readonly string Bar0 = "foo_bar0";

			public readonly string Bar1 = "foo_bar1";

			public readonly string Version = "foo_version";

			public FooWithVersionTable() : base("foo_with_version") { }
		}
	}
}
