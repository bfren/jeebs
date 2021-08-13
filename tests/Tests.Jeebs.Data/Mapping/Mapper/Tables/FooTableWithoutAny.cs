// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public record class FooTableWithoutAny : Table
	{
		public FooTableWithoutAny() : base("foo_without_any") { }
	}
}
