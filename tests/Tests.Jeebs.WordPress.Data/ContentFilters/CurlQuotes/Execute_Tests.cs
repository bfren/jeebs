﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
