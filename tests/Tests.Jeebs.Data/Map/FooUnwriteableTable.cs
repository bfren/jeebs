// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map;

public record class FooUnwriteableTable : Table
{
	public string FooId { get; } = "foo_unwriteable_id";

	public string Bar2 { get; } = "foo_unwriteable_bar2";

	public string Bar3 { get; } = "foo_unwriteable_bar3";

	public FooUnwriteableTable() : base("foo_unwriteable") { }
}
