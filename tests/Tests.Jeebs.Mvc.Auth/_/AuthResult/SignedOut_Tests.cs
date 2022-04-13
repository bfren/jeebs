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

		// Act
		var result = new AuthResult.SignedOut();

		// Assert
		Assert.True(result.Success);
		Assert.Equal(AlertType.Success, result.Message.Type);
		Assert.Equal(nameof(AlertType.Success), result.Message.Text);
		Assert.Null(result.RedirectTo);
		Assert.Equal(200, result.StatusCode);
		Assert.True(result.Value);
	}
}
