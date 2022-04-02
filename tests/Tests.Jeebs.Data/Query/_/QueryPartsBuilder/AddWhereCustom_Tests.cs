// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Data.Query.QueryPartsBuilder_Tests.Setup;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public class AddWhereCustom_Tests : AddWhereCustom_Tests<TestBuilder, TestId>
{
	protected override TestBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Theory]
	[MemberData(nameof(Test00_Data))]
	public override void Test00_Clause_Null_Or_Empty_Returns_None_With_TryingToAddEmptyClauseToWhereCustomMsg(string input) =>
		Test00(input);

	[Theory]
	[MemberData(nameof(Test01_Data))]
	public override void Test01_Invalid_Parameters_Returns_None_With_UnableToAddParametersToWhereCustomMsg(object input) =>
		Test01(input);

	[Fact]
	public override void Test02_Returns_New_Parts_With_Clause_And_Parameters() =>
		Test02();
}
