// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooTableWithoutBar0 : Table
	{
		public string FooId { get; } = "foo_id";

		public string Bar1 { get; } = "foo_bar1";

		public FooTableWithoutBar0() : base("foo_without_bar0") { }
	}
}
