// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.Auth.AuthResult_Tests;

public class SignedOut_Tests
{
	[Fact]
	public void Creates_With_Correct_Values()
	{
		// Arrange
		var url = Rnd.Str;

		// Act
		var result = new AuthResult.SignedOut(url);

		// Assert
		Assert.True(result.Success);
		Assert.Equal(AlertType.Success, result.Message.Type);
		Assert.Equal(nameof(AlertType.Success), result.Message.Text);
		Assert.Equal(200, result.StatusCode);
		Assert.Equal(url, result.Value);
	}
}
