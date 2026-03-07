// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Jwt.Constants;

namespace Jeebs.Auth.Jwt.Functions.JwtF_Tests;

public class GenerateKey_Tests
{
	[Fact]
	public void GenerateSigningKey_Returns_Key_Of_Signing_Key_Length()
	{
		// Arrange

		// Act
		var result = JwtF.GenerateSigningKey();

		// Assert
		Assert.Equal(JwtSecurity.SigningKeyBytes, result.Length);
	}

	[Fact]
	public void GenerateSigningKey_Returns_Key_With_All_Character_Classes()
	{
		// Arrange

		// Act
		var result = JwtF.GenerateSigningKey();

		// Assert
		Assert.False(result.All(c => Helpers.Chars.Lowercase.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Uppercase.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Numbers.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Special.Contains(c)));
		Assert.True(result.All(c => Helpers.Chars.All.Contains(c)));
	}

	[Fact]
	public void GenerateEncryptingKey_Returns_Key_Of_Encrypting_Key_Length()
	{
		// Arrange

		// Act
		var result = JwtF.GenerateEncryptingKey();

		// Assert
		Assert.Equal(JwtSecurity.EncryptingKeyBytes, result.Length);
	}

	[Fact]
	public void GenerateEncryptingKey_Returns_Key_With_All_Character_Classes()
	{
		// Arrange

		// Act
		var result = JwtF.GenerateEncryptingKey();

		// Assert
		Assert.False(result.All(c => Helpers.Chars.Lowercase.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Uppercase.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Numbers.Contains(c)));
		Assert.False(result.All(c => Helpers.Chars.Special.Contains(c)));
		Assert.True(result.All(c => Helpers.Chars.All.Contains(c)));
	}
}
