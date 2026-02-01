// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class AddRightJoin_Tests : AddRightJoin_Tests<TermsPartsBuilder, WpTermId>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Adds_Columns_To_RightJoin() =>
		Test00();
}
