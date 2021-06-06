// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class ReplaceHtmlTags_Tests
	{
		public static IEnumerable<object[]> String_Returns_Value_With_Html_Tags_Replaced_Data()
		{
			yield return new object[] { "<p>Ben</p>", "Ben" };
			yield return new object[] { "<p class=\"attr\">Ben</p>", "Ben" };
			yield return new object[] { "<p class=\"attr\">Ben <strong>Green</strong></p>", "Ben Green" };
		}

		[Theory]
		[InlineData(null)]
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
}
