// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.PostType_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.PostTypeTypeHandler_Tests;

public class Parse_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_PostType_Data), MemberType = typeof(Base))]
	public void Valid_Value_Returns_PostType(string input, PostType expected)
	{
		// Arrange
		var handler = new PostTypeTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Same(expected, result);
	}

	[Fact]
	public void Null_Value_Returns_Post_PostType()
	{
		// Arrange
		var handler = new PostTypeTypeHandler();

		// Act
		var result = handler.Parse(null!);

		// Assert
		Assert.Same(PostType.Post, result);
	}

	[Fact]
	public void Invalid_Value_Returns_Post_PostType()
	{
		// Arrange
		var value = Rnd.Str;
		var handler = new PostTypeTypeHandler();

		// Act
		var result = handler.Parse(value);

		// Assert
		Assert.Same(PostType.Post, result);
	}
}
