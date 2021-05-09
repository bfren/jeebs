// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddSort_Tests : AddSort<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Fact]
		public override void Test00_SortRandom_True_Returns_New_Parts_With_SortRandom_True() =>
			Test00();

		[Fact]
		public override void Test01_SortRandom_True_And_Sort_Not_Null_Returns_New_Parts_With_SortRandom_True() =>
			Test01();

		[Fact]
		public override void Test02_SortRandom_False_And_Sort_Not_Null_Returns_New_Parts_With_Sort() =>
			Test02();

		[Fact]
		public override void Test03_SortRandom_False_And_Sort_Null_Returns_Original_Parts() =>
			Test03();
	}
}
