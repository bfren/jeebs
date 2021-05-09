// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class GetParts_Tests : GetParts<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Fact]
		public override void Test00_Adds_Id() =>
			Test00();

		[Fact]
		public override void Test01_Adds_Ids() =>
			Test01();

		[Fact]
		public override void Test02_Adds_SortRandom() =>
			Test02();

		[Fact]
		public override void Test03_Adds_Sort() =>
			Test03();
	}
}
