// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Data.AuthUserRepository_Tests;

public class NormaliseEmail_Tests
{
	[Theory]
	[InlineData("ben@example.com")]
	[InlineData("Ben@Example.com")]
	[InlineData("  BEN@EXAMPLE.COM\t")]
	public void Returns_Trimmed_Lower_Case(string email)
	{
		// Arrange

		// Act
		var result = AuthUserRepository.NormaliseEmail(email);

		// Assert
		Assert.Equal("ben@example.com", result);
	}
}
