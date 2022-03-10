// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WordPress.Data.Enums.CommentType_Tests;

public class Parse_Tests
{
	public static TheoryData<string, CommentType> Returns_Correct_CommentType_Data =>
		new()
		{
			{ string.Empty, CommentType.Blank },
			{ "pingback", CommentType.Pingback }
		};

	[Theory]
	[MemberData(nameof(Returns_Correct_CommentType_Data))]
	public void Returns_Correct_CommentType(string name, CommentType type)
	{
		// Arrange

		// Act
		var result = CommentType.Parse(name);

		// Assert
		Assert.Same(type, result);
	}

	[Fact]
	public void Unknown_Returns_Blank()
	{
		// Arrange
		var name = F.Rnd.Str;

		// Act
		var result = CommentType.Parse(name);

		// Assert
		Assert.Same(CommentType.Blank, result);
	}
}
