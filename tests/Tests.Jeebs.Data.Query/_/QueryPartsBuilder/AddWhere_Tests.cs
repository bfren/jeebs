﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Data.Query.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public class AddWhere_Tests : AddWhere_Tests<TestBuilder, TestId>
{
	protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public override void Test00_Column_Exists_Adds_Where() =>
		Test00();

	[Fact]
	public override void Test01_Column_Does_Not_Exist_Returns_None_With_PropertyDoesNotExistOnTypeMsg() =>
		Test01();
}
