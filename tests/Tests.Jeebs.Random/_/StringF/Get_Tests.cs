// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq;
using Xunit;
using static F.Rnd.StringF;

namespace F.StringF_Tests;

public class Get_Tests
{
	[Fact]
	public void Valid_Length_Returns_String()
	{
		// Arrange
		const int length = 10;

		// Act
		var result = Get(length);

		// Assert
		Assert.Equal(length, result.Length);
	}

	[Fact]
	public void Invalid_Options_Throws_InvalidOperationException()
	{
		// Arrange
		const int length = 3;

		// Act
		var action = void () => Get(length, options: new(false, false, false, false));

		// Assert
		_ = Assert.Throws<InvalidOperationException>(action);
	}

	[Fact]
	public void Invalid_Length_Throws_InvalidOperationException()
	{
		// Arrange
		const int length = 3;

		// Act
		var action = void () => Get(length, options: new(true, true, true, true));

		// Assert
		_ = Assert.Throws<InvalidOperationException>(action);
	}

	[Fact]
	public void Returns_Only_Lowercase_Characters()
	{
		// Arrange

		// Act
		var result = Get(12, options:new(true, false, false, false));

		// Assert
		Assert.True(result.All(c => LowercaseChars.Contains(c)));
	}

	[Fact]
	public void Returns_Only_Uppercase_Characters()
	{
		// Arrange

		// Act
		var result = Get(64, options: new(false, true, false, false));

		// Assert
		Assert.True(result.All(c => UppercaseChars.Contains(c)));
	}

	[Fact]
	public void Returns_Only_Numeric_Characters()
	{
		// Arrange

		// Act
		var result = Get(64, options: new(false, false, true, false));

		// Assert
		Assert.True(result.All(c => NumberChars.Contains(c)));
	}

	[Fact]
	public void Returns_Only_Special_Characters()
	{
		// Arrange

		// Act
		var result = Get(64, options: new(false, false, false, true));

		// Assert
		Assert.True(result.All(c => SpecialChars.Contains(c)));
	}

	[Fact]
	public void Returns_String_With_All_Characters()
	{
		// Arrange

		// Act
		var result = Get(64, options: new(true, true, true, true));

		// Assert
		Assert.False(result.All(c => LowercaseChars.Contains(c)));
		Assert.False(result.All(c => UppercaseChars.Contains(c)));
		Assert.False(result.All(c => NumberChars.Contains(c)));
		Assert.False(result.All(c => SpecialChars.Contains(c)));
		Assert.True(result.All(c => AllChars.Contains(c)));
	}
}
