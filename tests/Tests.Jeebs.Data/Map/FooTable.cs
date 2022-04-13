// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Map;

public record class FooTable : Table
{
	[Id]
	public string FooId { get; } = "foo_id";

	public string Bar0 { get; } = "foo_bar0";

	public string Bar1 { get; } = "foo_bar1";

	public FooTable() : base("foo") { }

	public FooTable(string name) : base(name) { }
}
