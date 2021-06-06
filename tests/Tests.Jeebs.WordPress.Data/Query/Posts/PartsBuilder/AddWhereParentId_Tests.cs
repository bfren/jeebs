// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public class AddWhereParentId_Tests : QueryPartsBuilder_Tests<Query.PostsPartsBuilder, WpPostId>
	{
		protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Theory]
		[InlineData(null)]
		[InlineData(0)]
		[InlineData(-1)]
		public void Invalid_ParentId_Does_Nothing(long? input)
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereParentId(v.Parts, input);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Adds_ParentId_Equal()
		{
			// Arrange
			var (builder, v) = Setup();
			var parentId = F.Rnd.NumberF.GetInt64(1, max: 1000);

			// Act
			var result = builder.AddWhereParentId(v.Parts, parentId);

			// Assert
			AssertWhere(v.Parts, result, Post.ParentId, Compare.Equal, parentId);
		}
	}
}
