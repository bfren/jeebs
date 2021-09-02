// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddLeftJoin_Tests : AddLeftJoin_Tests<TestBuilder, TestId>
	{
		protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Adds_Columns_To_LeftJoin() =>
			Test00();
	}
}
