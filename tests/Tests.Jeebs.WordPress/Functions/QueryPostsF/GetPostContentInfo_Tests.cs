// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities;
using static Jeebs.WordPress.Functions.QueryPostsF.M;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class GetPostContentInfo_Tests
{
	[Fact]
	public void No_Content_Property_Returns_None_With_ContentPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContentInfo<NoContentProperty>();

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<ContentPropertyNotFoundMsg<NoContentProperty>>(none);
	}

	[Fact]
	public void Content_Property_Wrong_Type_Returns_None_With_ContentPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContentInfo<WithContentPropertyWrongType>();

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<ContentPropertyNotFoundMsg<WithContentPropertyWrongType>>(none);
	}

	[Fact]
	public void With_Content_Property_Returns_Content_Property()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContentInfo<WithContentProperty>();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(nameof(WpPostEntity.Content), some.Name);
	}

	public sealed record class NoContentProperty;

	public sealed record class WithContentPropertyWrongType(int Content);

	public sealed record class WithContentProperty(string Content);
}
