// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static Jeebs.Data.Querying.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class GetColumns_Tests : GetColumns_Tests<TestBuilder, TestId>
	{
		protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Calls_Extract_From() =>
			Test00<TestModel>();

		public record TestModel;
	}
}
