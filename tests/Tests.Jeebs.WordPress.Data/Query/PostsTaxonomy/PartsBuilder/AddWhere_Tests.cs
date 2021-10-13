// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests;

public class AddWhere_Tests : AddWhere_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
{
	protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Column_Exists_Adds_Where() =>
		Test00();

	[Fact]
	public override void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg() =>
		Test01();
}
