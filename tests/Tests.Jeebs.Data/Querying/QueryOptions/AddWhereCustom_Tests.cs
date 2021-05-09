// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddWhereCustom_Tests : AddWhereCustom<TestOptions, TestId>
	{
		protected override TestOptions Create(IMapper mapper) =>
			new(mapper);

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public override void Test00_Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input) =>
			Test00(input);

		[Theory]
		[InlineData(null)]
		[InlineData(42)]
		[InlineData(true)]
		[InlineData('c')]
		public override void Test01_Invalid_Parameters_Returns_None_With_UnableToAddParametersToWhereCustomMsg(object input) =>
			Test01(input);

		[Fact]
		public override void Test02_Returns_New_Parts_With_Clause_And_Parameters() =>
			Test02();
	}
}
