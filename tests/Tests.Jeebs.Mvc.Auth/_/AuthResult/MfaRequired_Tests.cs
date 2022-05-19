// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using static Jeebs.Mvc.Auth.AuthResult.M;

namespace Jeebs.Mvc.Auth.AuthResult_Tests;

public class MfaRequired_Tests
{
	[Fact]
	public void Creates_With_Correct_Values()
	{
		// Arrange

		// Act
		var result = new AuthResult.MfaRequired();

		// Assert
		Assert.False(result.Success);
		Assert.Equal(AlertType.Error, result.Message.Type);
		Assert.Equal(typeof(MfaRequiredMsg).ToString(), result.Message.Text);
		Assert.Equal(401, result.StatusCode);
		Assert.Null(result.Value);
	}
}
