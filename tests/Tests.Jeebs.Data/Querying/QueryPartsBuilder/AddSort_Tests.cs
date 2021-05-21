﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddSort_Tests : AddSort_Tests<TestBuilder, TestId>
	{
		protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
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
}
