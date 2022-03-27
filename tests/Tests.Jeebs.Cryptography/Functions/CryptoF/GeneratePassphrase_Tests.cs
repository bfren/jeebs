// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Cryptography.Functions.CryptoF.M;

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public class GeneratePassphrase_Tests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	[InlineData(4)]
	public void NumberOfWords_Less_Than_Five_Returns_None_With_CryptographicallySecurePassphrasesMustContainAtLeastFiveWordsMsg(int input)
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase(input);

		// Assert
		var none = result.AssertNone().AssertType<CryptographicallySecurePassphrasesMustContainAtLeastFiveWordsMsg>();
		Assert.Equal(input, none.Value);
	}

	[Fact]
	public void Uses_Eight_Words()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var some = result.AssertSome().Split('-');
		Assert.Equal(8, some.Length);
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
		var some = result.AssertSome().Split('-');
		Assert.Equal(input, some.Length);
	}

	[Fact]
	public void Makes_First_Letters_Upper_Case()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var some = result.AssertSome();
		Assert.NotEqual(some, some.ToLower());
	}

	[Fact]
	public void Includes_A_Number()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var some = result.AssertSome();
		Assert.Contains(some, x => char.IsNumber(x));
	}
}
