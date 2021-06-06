// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public record FooTableWithoutBar0 : Table
	{
		public string FooId { get; } = "foo_id";

		public string Bar1 { get; } = "foo_bar1";

		public FooTableWithoutBar0() : base("foo_without_bar0") { }
	}
}
