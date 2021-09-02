// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests
{
	public class AddLeftJoin_Tests : AddLeftJoin_Tests<Query.PostsMetaPartsBuilder, WpPostMetaId>
	{
		protected override Query.PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public override void Test00_Adds_Columns_To_LeftJoin() =>
			Test00();
	}
}
