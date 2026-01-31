// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public class AddLeftJoin_Tests : AddLeftJoin_Tests<PostsMetaPartsBuilder, WpPostMetaId>
{
	protected override PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Adds_Columns_To_LeftJoin() =>
		Test00();
}
