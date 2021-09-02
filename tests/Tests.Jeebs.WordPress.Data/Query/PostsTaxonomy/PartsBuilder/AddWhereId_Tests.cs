// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests
{
	public class AddWhereId_Tests : AddWhereId_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId>
	{
		protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Id_And_Ids_Null_Returns_Original_Parts() =>
			Test00();

		[Fact]
		public override void Test01_Id_Set_Adds_Where_Id_Equal() =>
			Test01();

		[Fact]
		public override void Test02_Id_And_Ids_Set_Adds_Where_Id_Equal() =>
			Test02();

		[Fact]
		public override void Test03_Id_Null_Ids_Set_Adds_Where_Id_In() =>
			Test03();
	}
}
