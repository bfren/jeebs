// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Mvc.Auth.Functions.JwtF.M;

namespace Jeebs.Mvc.Auth.Functions.JwtF_Tests;

public class GetToken_Tests
{
	[Fact]
	public void Invalid_Authorization_Header_Returns_None_With_InvalidAuthorisationHeaderMsg()
	{
		// Arrange
		var header = Rnd.Str;

		// Act
		var result = JwtF.GetToken(header);

		// Assert
		result.AssertNone().AssertType<InvalidAuthorisationHeaderMsg>();
	}

	[Fact]
	public void Returns_Token()
	{
		// Arrange
		var value = Rnd.Str;
		var header = $"Bearer {value}";

		// Act
		var result = JwtF.GetToken(header);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}
