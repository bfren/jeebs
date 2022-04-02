﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Entities.StrongIds;
using static Jeebs.WordPress.Query.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public class AddWherePublishedTo_Tests : QueryPartsBuilder_Tests<PostsPartsBuilder, WpPostId>
{
	protected override PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
		GetBuilder(extract);

	[Fact]
	public void To_Null_Does_Nothing()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWherePublishedTo(v.Parts, null);

		// Assert
		var some = result.AssertSome();
		Assert.Same(v.Parts, some);
	}

	[Fact]
	public void Adds_Published_MoreThanOrEqual_From()
	{
		// Arrange
		var (builder, v) = Setup();
		var to = Rnd.DateTime;
		var expectedTo = to.EndOfDay().ToMySqlString();

		// Act
		var result = builder.AddWherePublishedTo(v.Parts, to);

		// Assert
		AssertWhere(v.Parts, result, Post.PublishedOn, Compare.LessThanOrEqual, expectedTo);
	}
}
