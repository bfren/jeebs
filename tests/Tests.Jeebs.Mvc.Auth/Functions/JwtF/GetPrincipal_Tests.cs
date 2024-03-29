// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;

namespace Jeebs.Mvc.Auth.Functions.JwtF_Tests;

public class GetPrincipal_Tests
{
	[Fact]
	public void GetPrincipal_Calls_Auth_ValidateToken()
	{
		// Arrange
		var auth = Substitute.For<IAuthJwtProvider>();
		var value = Rnd.Str;

		// Act
		_ = JwtF.GetPrincipal(auth, value);

		// Assert
		auth.Received().ValidateToken(value);
	}
}
