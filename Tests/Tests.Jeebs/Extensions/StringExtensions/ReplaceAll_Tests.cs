using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class StringExtensions_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceAll_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
			var result = input.ReplaceAll(null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben Green", new[] { "e", "n" }, null, "B Gr")]
		[InlineData("Ben Green", new[] { "e", "n" }, "-", "B-- Gr---")]
		public void ReplaceAll_String_ReturnsValueWithStringsReplaced(string input, string[] replace, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceAll(replace, with);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
