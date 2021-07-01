// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public class AddWhereCustom_Tests : AddWhereCustom_Tests<Query.TermsPartsBuilder, WpTermId>
	{
		protected override Query.TermsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
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
}
