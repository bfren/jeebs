// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.Functions;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public class AddWherePostId_Tests : QueryPartsBuilder_Tests<PostsMetaPartsBuilder, WpPostMetaId>
{
	protected override PostsMetaPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Invalid_PostId_And_PostIds_Empty_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWherePostId(v.Parts, null, Substitute.For<IImmutableList<WpPostId>>());

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Adds_PostId_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var postId = IdGen.ULongId<WpPostId>();

		// Act
		var result = builder.AddWherePostId(v.Parts, postId, Substitute.For<IImmutableList<WpPostId>>()).Unsafe().Unwrap();

		// Assert
		AssertWhere(v.Parts, result, PostsMeta.PostId, Compare.Equal, postId.Value);
	}

	[Fact]
	public void Adds_PostIds_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var id0 = IdGen.ULongId<WpPostId>();
		var id1 = IdGen.ULongId<WpPostId>();
		var postIds = ListF.Create(id0, id1);
		var postIdValues = postIds.Select(p => p.Value);

		// Act
		var result = builder.AddWherePostId(v.Parts, null, postIds).Unsafe().Unwrap();

		// Assert
		AssertWhere(v.Parts, result, PostsMeta.PostId, Compare.In, postIdValues);
	}
}
