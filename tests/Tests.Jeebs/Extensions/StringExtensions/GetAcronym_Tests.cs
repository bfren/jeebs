// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class GetAcronym_Tests
	{
		[Theory]
		[InlineData("XML", "XML")]
		[InlineData("eXtensible Markup Language", "XML")]
		[InlineData("Jean-Luc Picard", "JLP")]
		public void Generates_Acronym(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.GetAcronym();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
