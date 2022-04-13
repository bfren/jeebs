// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using static Jeebs.Mvc.Auth.AuthResult.M;

namespace Jeebs.Mvc.Auth.AuthResult_Tests;

public class Denied_Tests
{
	[Fact]
	public void Creates_With_Correct_Values()
	{
		// Arrange

		// Act
		var result = new AuthResult.Denied();

		// Assert
		Assert.False(result.Success);
		Assert.Equal(AlertType.Error, result.Message.Type);
		Assert.Equal(typeof(DeniedMsg).ToString(), result.Message.Text);
		Assert.Null(result.RedirectTo);
		Assert.Equal(401, result.StatusCode);
		Assert.False(result.Value);
	}
}