// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.StringExtensions_Tests;
using Xunit;

namespace Jeebs.WordPress.Data.ContentFilters.CurlQuotes_Tests
{
	public class Execute_Tests
	{
		[Theory]
		[MemberData(nameof(ConvertInnerHtmlQuotes_Tests.Html_Returns_Html_With_Converted_Quotes_Data), MemberType = typeof(ConvertInnerHtmlQuotes_Tests))]
		public void Converts_Quotes(string input, string expected)
		{
			// Arrange

			// Act
			var result = CurlQuotes.Create().Execute(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
