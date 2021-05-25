// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public class AddWherePublishedFrom_Tests : QueryPartsBuilder_Tests<Query.PostsPartsBuilder, WpPostId>
	{
		protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void From_Null_Does_Nothing()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWherePublishedFrom(v.Parts, null);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Adds_Published_MoreThanOrEqual_From()
		{
			// Arrange
			var (builder, v) = Setup();
			var from = F.Rnd.DateTime;
			var expectedFrom = from.StartOfDay().ToMySqlString();

			// Act
			var result = builder.AddWherePublishedFrom(v.Parts, from);

			// Assert
			AssertWhere(v.Parts, result, Post.PublishedOn, Compare.MoreThanOrEqual, expectedFrom);
		}
	}
}
