// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class GetColumns_Tests : GetColumns_Tests<TestBuilder, TestId>
	{
		protected override TestBuilder GetConfiguredBuilder() =>
			GetBuilder();

		[Fact]
		public override void Test00_No_Matching_Properties_Returns_Empty_List() =>
			Test00<NoMatchingProperties>();

		[Fact]
		public override void Test01_Returns_Columns_For_Matching_Properties()
		{
			var column = F.Rnd.Str;
			Test01<WithMatchingProperties>(new TestTable0(F.Rnd.Str, column), column, nameof(TestTable0.Foo));
		}

		public record NoMatchingProperties;

		public record WithMatchingProperties(int Foo);
	}
}
