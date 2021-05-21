// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests
{
	public class AddInnerJoin_Tests : AddInnerJoin_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
	{
		protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Adds_Columns_To_InnerJoin() =>
			Test00();
	}
}
