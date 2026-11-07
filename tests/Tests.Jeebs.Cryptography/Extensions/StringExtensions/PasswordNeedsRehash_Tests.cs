// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography.StringExtensions_Tests;

public sealed class PasswordNeedsRehash_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Empty_Hash_Returns_True(string? input)
	{
		// Arrange

		// Act
		var result = input!.PasswordNeedsRehash();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Hash_With_Older_Settings_Returns_True()
	{
		// Arrange
		var hash = PasswordHash.ArgonHashString(Rnd.Str, PasswordHash.StrengthArgon.Moderate).TrimEnd('\0');

		// Act
		var result = hash.PasswordNeedsRehash();

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Hash_From_HashPassword_Returns_False()
	{
		// Arrange
		var hash = Rnd.Str.HashPassword();

		// Act
		var result = hash.PasswordNeedsRehash();

		// Assert
		Assert.False(result);
	}
}
