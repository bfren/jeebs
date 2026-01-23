// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public class GeneratePassphrase_Tests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	public void NumberOfWords_Less_Than_Three_Returns_Fail(int input)
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase(input);

		// Assert
		result.AssertFail(
			"Secure passphrases must contain at least three words ({Number} requested).",
			new { Number = input }
		);
	}

	[Fact]
	public void Uses_Eight_Words()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(8, ok.Split('-').Length);
	}

	[Theory]
	[InlineData(5)]
	[InlineData(9)]
	[InlineData(13)]
	public void Uses_Correct_Number_Of_Words(int input)
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase(input);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(input, ok.Split('-').Length);
	}

	[Fact]
	public void Makes_First_Letters_Upper_Case()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var ok = result.AssertOk();
		Assert.NotEqual(ok, ok.ToLowerInvariant());
	}

	[Fact]
	public void Includes_A_Number()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var ok = result.AssertOk();
		Assert.Contains(ok, char.IsNumber);
	}
}
