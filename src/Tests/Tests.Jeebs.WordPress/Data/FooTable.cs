// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Mapping;

namespace Jeebs.WordPress.Data
{
	public class FooTable : Table
	{
		public string FooId { get; } = "foo_id";

		public string Bar0 { get; } = "foo_bar0";

		public string Bar1 { get; } = "foo_bar1";

		public FooTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.FooTable) { }
	}
}
