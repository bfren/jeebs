// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public class Create_Tests : Create_Tests<PostsMetaPartsBuilder, WpPostMetaId, WpPostMetaEntity>
{
	protected override PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Returns_With_Table() =>
		Test00();

	[Fact]
	public override void Test01_Calls_Extract_From() =>
		Test01();

	[Fact]
	public override void Test02_Returns_With_Maximum() =>
		Test02();

	[Fact]
	public override void Test03_Returns_With_Skip() =>
		Test03();
}
