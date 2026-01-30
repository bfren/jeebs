// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data;

public record class FooTableWithBar2 : Table
{
	public string FooId { get; } = "foo_id";

	public string Bar0 { get; } = "foo_bar0";

	public string Bar1 { get; } = "foo_bar1";

	public string Bar2 { get; } = "foo_bar2";

	public FooTableWithBar2() : base("foo_with_bar2") { }
}
