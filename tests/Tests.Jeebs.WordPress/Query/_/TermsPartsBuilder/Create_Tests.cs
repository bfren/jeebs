// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public class Create_Tests : Create_Tests<TermsPartsBuilder, WpTermId, WpPostMetaEntity>
{
	protected override TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
