// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Auth.Functions.HttpF_Tests;

public class GetToken_Tests
{
	[Fact]
	public void Invalid_Authorization_Header_Returns_None_With_InvalidAuthorisationHeaderMsg()
	{
		// Arrange
		var header = Rnd.Str;

		// Act
		var result = HttpF.GetToken(header);

		// Assert
		result.AssertFailure("Invalid Authorization header: '{Header}'.", header);
	}

	[Fact]
	public void Returns_Token()
	{
		// Arrange
		var value = Rnd.Str;
		var header = $"Bearer {value}";

		// Act
		var result = HttpF.GetToken(header);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(value, ok.Value);
	}
}
