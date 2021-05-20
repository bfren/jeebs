// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddInnerJoin_Tests : AddInnerJoin_Tests<TestBuilder, TestId>
	{
		protected override TestBuilder GetConfiguredBuilder() =>
			GetBuilder();

		[Fact]
		public override void Test00_Adds_Columns_To_InnerJoin() =>
			Test00();
	}
}
