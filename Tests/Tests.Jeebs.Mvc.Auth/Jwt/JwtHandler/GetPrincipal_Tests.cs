// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth;
using Jm.Mvc.Auth.Jwt.JwtHandler;
using NSubstitute;
using Xunit;

namespace Jeebs.Mvc.Auth.Jwt.JwtHandler_Tests
{
	public class GetPrincipal_Tests
	{
		[Fact]
		public void GetPrincipal_Calls_Auth_ValidateToken()
		{
			// Arrange
			var auth = Substitute.For<IJwtAuthProvider>();
			var value = F.Rnd.Str;

			// Act
			JwtHandler.GetPrincipal(auth, value);

			// Assert
			auth.Received().ValidateToken(value);
		}
	}
}
