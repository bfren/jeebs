// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Linq;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using NSubstitute;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests
{
	public class AddWherePostId_Tests : QueryPartsBuilder_Tests<Query.PostsMetaPartsBuilder, WpPostMetaId>
	{
		protected override Query.PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void Invalid_PostId_And_PostIds_Empty_Does_Nothing()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWherePostId(v.Parts, null, Substitute.For<IImmutableList<WpPostId>>());

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Adds_PostId_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var postId = new WpPostId(F.Rnd.Ulng);

			// Act
			var result = builder.AddWherePostId(v.Parts, postId, Substitute.For<IImmutableList<WpPostId>>());

			// Assert
			AssertWhere(v.Parts, result, PostMeta.PostId, Compare.Equal, postId.Value);
		}

		[Fact]
		public void Adds_PostIds_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var id0 = new WpPostId(F.Rnd.Ulng);
			var id1 = new WpPostId(F.Rnd.Ulng);
			var postIds = ImmutableList.Create(id0, id1);
			var postIdValues = postIds.Select(p => p.Value);

			// Act
			var result = builder.AddWherePostId(v.Parts, null, postIds);

			// Assert
			AssertWhere(v.Parts, result, PostMeta.PostId, Compare.In, postIdValues);
		}
	}
}
