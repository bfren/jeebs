// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using Maybe.Testing;
using Xunit;
using static Jeebs.Cryptography.Functions.CryptoF.M;

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public class GeneratePassphrase_Tests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(1)]
	public void NumberOfWords_Less_Than_Two_Returns_None_With_NullPassphraseMsg(int input)
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase(input);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<NullPassphraseMsg>(none);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(4)]
	[InlineData(8)]
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
	public void Uses_Three_Words()
	{
		// Arrange

		// Act
		var result = CryptoF.GeneratePassphrase();

		// Assert
		var some = result.AssertSome().Split('-');
		Assert.Equal(3, some.Length);
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
