// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping
{
	public class FooUnwriteableTable : Table
	{
		public string FooId { get; } = "foo_unwriteable_id";

		public string Bar2 { get; } = "foo_unwriteable_bar2";

		public string Bar3 { get; } = "foo_unwriteable_bar3";

		public FooUnwriteableTable() : base("foo_unwriteable") { }
	}
}
