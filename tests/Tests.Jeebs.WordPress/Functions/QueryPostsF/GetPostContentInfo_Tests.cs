// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Entities;

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
		_ = result.AssertFailure("Content property not found on model '{Type}'.",
			nameof(NoContentProperty)
		);
	}

	[Fact]
	public void Content_Property_Wrong_Type_Returns_None_With_ContentPropertyNotFoundMsg()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContentInfo<WithContentPropertyWrongType>();

		// Assert
		_ = result.AssertFailure("Content property not found on model '{Type}'.",
			nameof(WithContentPropertyWrongType)
		);
	}

	[Fact]
	public void With_Content_Property_Returns_Content_Property()
	{
		// Arrange

		// Act
		var result = QueryPostsF.GetPostContentInfo<WithContentProperty>();

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(nameof(WpPostEntity.Content), ok.Name);
	}

	public sealed record class NoContentProperty;

	public sealed record class WithContentPropertyWrongType(int Content);

	public sealed record class WithContentProperty(string Content);
}
