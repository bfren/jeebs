// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.Auth.AuthResult_Tests;

public class SignedIn_Tests
{
	[Fact]
	public void Creates_With_Correct_Values()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new AuthResult.SignedIn(value);

		// Assert
		Assert.True(result.Success);
		Assert.Equal(AlertType.Success, result.Message.Type);
		Assert.Equal(nameof(AlertType.Success), result.Message.Text);
		Assert.Equal(value, result.RedirectTo);
		Assert.Equal(200, result.StatusCode);
		Assert.True(result.Value);
	}
}
