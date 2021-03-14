// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class ToASCII_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToASCII();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben Green", "&#66;&#101;&#110;&#32;&#71;&#114;&#101;&#101;&#110;")]
		[InlineData("&<>#'{$^?~", "&#38;&#60;&#62;&#35;&#39;&#123;&#36;&#94;&#63;&#126;")]
		[InlineData("£ü©§", "&#63;&#63;&#63;&#63;")]
		public void String_ReturnsASCIIEncodedValue(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToASCII();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
