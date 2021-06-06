// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests
{
	public class Create_Tests : Create_Tests<Query.PostsTaxonomyPartsBuilder, WpTermId, WpPostMetaEntity>
	{
		protected override Query.PostsTaxonomyPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Returns_With_Table() =>
			Test00();

		[Fact]
		public override void Test01_Calls_Extract_From() =>
			Test01();

		[Fact]
		public override void Test02_Returns_With_Maximum() =>
			Test02();

		[Fact]
		public override void Test03_Returns_With_Skip() =>
			Test03();
	}
}
