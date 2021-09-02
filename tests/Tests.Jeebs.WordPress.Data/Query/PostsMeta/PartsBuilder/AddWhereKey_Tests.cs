// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests
{
	public class AddWhereKey_Tests : QueryPartsBuilder_Tests<Query.PostsMetaPartsBuilder, WpPostMetaId>
	{
		protected override Query.PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void Null_Key_Does_Nothing()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereKey(v.Parts, null);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Adds_Key_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var key = F.Rnd.Str;

			// Act
			var result = builder.AddWhereKey(v.Parts, key);

			// Assert
			AssertWhere(v.Parts, result, PostMeta.Key, Compare.Equal, key);
		}
	}
}
