// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Mapper_Tests;

public record class FooTableWithoutBar0 : Table
{
	public string FooId { get; } = "foo_id";

	public string Bar1 { get; } = "foo_bar1";

	public FooTableWithoutBar0() : base("foo_without_bar0") { }
}
