// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests.Setup;
using static StrongId.Testing.Generator;

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
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_PostId_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var postId = ULongId<WpPostId>();

		// Act
		var result = builder.AddWherePostId(v.Parts, postId, Substitute.For<IImmutableList<WpPostId>>());

		// Assert
		AssertWhere(v.Parts, result, PostsMeta.PostId, Compare.Equal, postId.Value);
	}

	[Fact]
	public void Adds_PostIds_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var id0 = ULongId<WpPostId>();
		var id1 = ULongId<WpPostId>();
		var postIds = ImmutableList.Create(id0, id1);
		var postIdValues = postIds.Select(p => p.Value);

		// Act
		var result = builder.AddWherePostId(v.Parts, null, postIds);

		// Assert
		AssertWhere(v.Parts, result, PostsMeta.PostId, Compare.In, postIdValues);
	}
}
