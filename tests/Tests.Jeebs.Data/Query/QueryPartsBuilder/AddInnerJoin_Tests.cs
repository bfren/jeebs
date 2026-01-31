// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using static Jeebs.Data.Query.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public class AddInnerJoin_Tests : AddInnerJoin_Tests<TestBuilder, TestId>
{
	protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Adds_Columns_To_InnerJoin() =>
		Test00();
}
