// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Auth.Jwt.JwtHandler_Tests;

public class GetPrincipal_Tests
{
	[Fact]
	public void GetPrincipal_Calls_Auth_ValidateToken()
	{
		// Arrange
		var auth = Substitute.For<IAuthJwtProvider>();
		var value = F.Rnd.Str;

		// Act
		JwtHandler.GetPrincipal(auth, value);

		// Assert
		auth.Received().ValidateToken(value);
	}
}
