// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static StrongId.Testing.Generator;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWhereParentId_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Theory]
	[InlineData(null)]
	[InlineData(0L)]
	public void Invalid_ParentId_Does_Nothing(long? input)
	{
		// Arrange
		var (builder, v) = Setup();
		WpPostId? id = input is null ? null : new() { Value = input.Value };

		// Act
		var result = builder.AddWhereParentId(v.Parts, id);

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_ParentId_Equal()
	{
		// Arrange
		var (builder, v) = Setup();
		var parentId = LongId<WpPostId>();

		// Act
		var result = builder.AddWhereParentId(v.Parts, parentId);

		// Assert
		AssertWhere(v.Parts, result, Post.ParentId, Compare.Equal, parentId.Value);
	}
}
