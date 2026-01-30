// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data;

public record class FooTableWithoutAny : Table
{
	public FooTableWithoutAny() : base("foo_without_any") { }
}
