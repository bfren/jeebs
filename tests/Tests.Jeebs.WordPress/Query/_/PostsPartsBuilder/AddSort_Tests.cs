﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddSort_Tests : AddSort_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True() =>
		Test00();

	[Fact]
	public override void Test01_SortRandom_False_With_Sort_Returns_New_Parts_With_Sort() =>
		Test01();

	[Fact]
	public override void Test02_SortRandom_False_And_Sort_Empty_Returns_Original_Parts() =>
		Test02();
}
