// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class Create_Tests : Create_Tests<TestBuilder, TestId, TestModel>
	{
		protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
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
}
