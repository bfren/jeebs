// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Enums;
using Base = Jeebs.WordPress.Enums.CommentType_Tests.Parse_Tests;

namespace Jeebs.WordPress.TypeHandlers.CommentTypeTypeHandler_Tests;

public class Parse_Tests
{
	[Theory]
	[MemberData(nameof(Base.Returns_Correct_CommentType_Data), MemberType = typeof(Base))]
	public void Valid_Value_Returns_CommentType(string input, CommentType expected)
	{
		// Arrange
		var handler = new CommentTypeTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Same(expected, result);
	}

	[Fact]
	public void Null_Value_Returns_Blank_CommentType()
	{
		// Arrange
		var handler = new CommentTypeTypeHandler();

		// Act
		var result = handler.Parse(null!);

		// Assert
		Assert.Same(CommentType.Blank, result);
	}

	[Fact]
	public void Invalid_Value_Returns_Blank_CommentType()
	{
		// Arrange
		var value = Rnd.Str;
		var handler = new CommentTypeTypeHandler();

		// Act
		var result = handler.Parse(value);

		// Assert
		Assert.Same(CommentType.Blank, result);
	}
}
