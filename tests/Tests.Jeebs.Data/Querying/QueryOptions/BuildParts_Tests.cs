// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class BuildParts_Tests : BuildParts<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Fact]
		public override void Test00_Returns_New_QueryParts_With_Where_Id() =>
			Test00();

		[Fact]
		public override void Test01_Returns_New_QueryParts_With_Where_Ids() =>
			Test01();

		[Fact]
		public override void Test02_Returns_New_QueryParts_With_Sort_Random() =>
			Test02();

		[Fact]
		public override void Test03_Returns_New_QueryParts_With_Sort() =>
			Test03();
	}
}
