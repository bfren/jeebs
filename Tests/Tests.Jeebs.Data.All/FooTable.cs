using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class FooTable : Table
	{
		public readonly string FooId = "foo_id";

		public readonly string Bar0 = "foo_bar0";

		public readonly string Bar1 = "foo_bar1";

		public FooTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.FooTable) { }
	}
}
