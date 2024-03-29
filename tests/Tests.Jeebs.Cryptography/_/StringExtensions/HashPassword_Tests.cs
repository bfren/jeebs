// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.StringExtensions_Tests;

public sealed class HashPassword_Tests
{
	private readonly string password = "Password to hash.";

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void HashPassword_NullOrWhiteSpace_Returns_Empty_String(string input)
	{
		// Arrange

		// Act
		var result = input.HashPassword();

		// Assert
		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void HashPassword_String_ReturnsUniqueValue()
	{
		// Arrange
		var input = password;

		// Act
		var hash1 = input.HashPassword();
		var hash2 = input.HashPassword();

		// Assert
		Assert.NotEqual(hash1, hash2);
	}
}
