// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class ReplaceHtmlTags_Tests
{
	public static TheoryData<string, string> String_Returns_Value_With_Html_Tags_Replaced_Data() =>
		new()
		{
			{ "<p>Ben</p>", "Ben" },
			{ "<p class=\"attr\">Ben</p>", "Ben" },
			{ "<p class=\"attr\">Ben <strong>Green</strong></p>", "Ben Green" }
		};

	[Theory]
	[InlineData("")]
	public void Null_Or_Empty_Returns_Original(string input)
	{
		// Arrange

		// Act
		var result = input.ReplaceHtmlTags();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[MemberData(nameof(String_Returns_Value_With_Html_Tags_Replaced_Data))]
	public void String_Returns_Value_With_Html_Tags_Replaced(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ReplaceHtmlTags();

		// Assert
		Assert.Equal(expected, result);
	}
}
