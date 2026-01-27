// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using Jeebs.WordPress.Enums;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWhereStatus_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void Adds_Status_To_Where()
	{
		// Arrange
		var (builder, v) = Setup();
		var status = new PostStatus(Rnd.Str);

		// Act
		var result = builder.AddWhereStatus(v.Parts, status).Unsafe().Unwrap();

		// Assert
		AssertWhere(v.Parts, result, Post.Status, Compare.Equal, status);
	}
}
