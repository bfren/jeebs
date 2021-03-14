// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class FooTable : Table
	{
		public string FooId { get; } = "foo_id";

		public string Bar0 { get; } = "foo_bar0";

		public string Bar1 { get; } = "foo_bar1";

		public FooTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.FooTable) { }
	}
}
