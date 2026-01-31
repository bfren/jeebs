// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.Ids;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWherePublishedFrom_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void From_Null_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWherePublishedFrom(v.Parts, null);

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	[Fact]
	public void Adds_Published_MoreThanOrEqual_From()
	{
		// Arrange
		var (builder, v) = Setup();
		var from = Rnd.DateTime;
		var expectedFrom = from.StartOfDay().ToMySqlString();

		// Act
		var result = builder.AddWherePublishedFrom(v.Parts, from).Unsafe().Unwrap();

		// Assert
		AssertWhere(v.Parts, result, Post.PublishedOn, Compare.MoreThanOrEqual, expectedFrom);
	}
}
