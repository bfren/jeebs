// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping
{
	public record FooUnwriteableTable : Table
	{
		public string FooId { get; } = "foo_unwriteable_id";

		public string Bar2 { get; } = "foo_unwriteable_bar2";

		public string Bar3 { get; } = "foo_unwriteable_bar3";

		public FooUnwriteableTable() : base("foo_unwriteable") { }
	}
}
