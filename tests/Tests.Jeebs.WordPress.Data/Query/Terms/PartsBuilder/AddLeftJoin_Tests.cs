// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public class AddLeftJoin_Tests : AddLeftJoin_Tests<Query.TermsPartsBuilder, WpTermId>
	{
		protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Adds_Columns_To_LeftJoin() =>
			Test00();
	}
}
