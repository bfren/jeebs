// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class ConvertInnerHtmlQuotes_Tests
	{
		public static IEnumerable<object[]> Html_Returns_Html_With_Converted_Quotes_Data()
		{
			yield return new object[] { "<a href=\"test\">'Ben'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;</a>" };
			yield return new object[] { "<a href=\"test\">'Ben'</a> 'Green'", "<a href=\"test\">&lsquo;Ben&rsquo;</a> &lsquo;Green&rsquo;" };
			yield return new object[] { "<a href=\"test\">'Ben's Test'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;s Test&rsquo;</a>" };
			yield return new object[] { "<a href=\"test\">\"Ben\"</a>", "<a href=\"test\">&ldquo;Ben&rdquo;</a>" };
			yield return new object[] { "<a href=\"test\">\"Ben\"</a> \"Green\"", "<a href=\"test\">&ldquo;Ben&rdquo;</a> &ldquo;Green&rdquo;" };
			yield return new object[] { "<a href=\"test\">\"Ben's Test\"</a>", "<a href=\"test\">&ldquo;Ben&rsquo;s Test&rdquo;</a>" };
		}

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
}
