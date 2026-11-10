// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Auth.Functions.HttpF_Tests;

public class GetToken_Tests
{
	[Fact]
	public void Invalid_Authorization_Header_Returns_Failure_Without_Header_Value()
	{
		// Arrange
		var header = Rnd.Str;

		// Act
		var result = HttpF.GetToken(header);

		// Assert
		result.AssertFailure("Authorization header is not a Bearer token.");
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
