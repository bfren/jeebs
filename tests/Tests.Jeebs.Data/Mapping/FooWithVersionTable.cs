// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping
{
	public record FooWithVersionTable : Table
	{
		public string FooId { get; } = "foo_with_version_id";

		public string Bar0 { get; } = "foo_with_version_bar0";

		public string Bar1 { get; } = "foo_with_version_bar1";

		public string Version { get; } = "foo_with_version_version";

		public FooWithVersionTable() : base("foo_with_version") { }
	}
}
