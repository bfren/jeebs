// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

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
