// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class ConvertInnerHtmlQuotes_Tests
{
	public static TheoryData<string, string> Html_Returns_Html_With_Converted_Quotes_Data() =>
		new()
		{
			{ "<a href=\"test\">'Ben'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;</a>" },
			{ "<a href=\"test\">'Ben'</a> 'Green'", "<a href=\"test\">&lsquo;Ben&rsquo;</a> &lsquo;Green&rsquo;" },
			{ "<a href=\"test\">'Ben's Test'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;s Test&rsquo;</a>" },
			{ "<a href=\"test\">\"Ben\"</a>", "<a href=\"test\">&ldquo;Ben&rdquo;</a>" },
			{ "<a href=\"test\">\"Ben\"</a> \"Green\"", "<a href=\"test\">&ldquo;Ben&rdquo;</a> &ldquo;Green&rdquo;" },
			{ "<a href=\"test\">\"Ben's Test\"</a>", "<a href=\"test\">&ldquo;Ben&rsquo;s Test&rdquo;</a>" }
		};

	[Theory]
	[MemberData(nameof(Html_Returns_Html_With_Converted_Quotes_Data))]
	public void Html_Returns_Html_With_Converted_Quotes(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ConvertInnerHtmlQuotes();

		// Assert
		Assert.Equal(expected, result);
	}
}
