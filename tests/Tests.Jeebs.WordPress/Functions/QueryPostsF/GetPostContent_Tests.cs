// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities;
using static Jeebs.WordPress.Functions.QueryPostsF.M;

namespace Jeebs.WordPress.Functions.QueryPostsF_Tests;

public class GetPostContent_Tests
{
	[Fact]
	public void No_Content_Property_Returns_None_With_ContentPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContent<NoContentProperty>();

		// Assert
		result.AssertNone().AssertType<ContentPropertyNotFoundMsg<NoContentProperty>>();
	}

	[Fact]
	public void Content_Property_Wrong_Type_Returns_None_With_ContentPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContent<WithContentPropertyWrongType>();

		// Assert
		result.AssertNone().AssertType<ContentPropertyNotFoundMsg<WithContentPropertyWrongType>>();
	}

	[Fact]
	public void With_Content_Property_Returns_Content_Property()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContent<WithContentProperty>();

		// Assert
		var some = result.AssertSome();
		Assert.Equal(nameof(WpPostEntity.Content), some.Name);
		Assert.Equal(typeof(string), some.PropertyType);
	}

	public sealed record class NoContentProperty;

	public sealed record class WithContentPropertyWrongType(int Content);

	public sealed record class WithContentProperty(string Content);
}
