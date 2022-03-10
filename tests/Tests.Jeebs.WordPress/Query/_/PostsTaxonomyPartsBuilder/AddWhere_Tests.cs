// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public class AddWhere_Tests : AddWhere_Tests<PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Column_Exists_Adds_Where() =>
		Test00();

	[Fact]
	public override void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg() =>
		Test01();
}
