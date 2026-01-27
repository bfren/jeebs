// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.PostStatus_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.PostStatusTypeHandler_Tests;

public class Parse_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_PostStatus_Data), MemberType = typeof(Base))]
	public void Valid_Value_Returns_PostStatusType(string input, PostStatus expected)
	{
		// Arrange
		var handler = new PostStatusTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Same(expected, result);
	}

	[Fact]
	public void Null_Value_Returns_Draft_PostStatus()
	{
		// Arrange
		var handler = new PostStatusTypeHandler();

		// Act
		var result = handler.Parse(null!);

		// Assert
		Assert.Same(PostStatus.Draft, result);
	}

	[Fact]
	public void Invalid_Value_Returns_Draft_PostStatus()
	{
		// Arrange
		var value = Rnd.Str;
		var handler = new PostStatusTypeHandler();

		// Act
		var result = handler.Parse(value);

		// Assert
		Assert.Same(PostStatus.Draft, result);
	}
}
