// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using System.Security.Principal;
using Jeebs.Config.Web.Auth.Jwt;

namespace Jeebs.Auth.Jwt.Functions.JwtF_Tests;

public class ValidateToken_Tests
{
	private static (JwtConfig config, JsonWebToken token, string user) GetToken(bool encrypt, DateTime notBefore, DateTime expires)
	{
		var config = encrypt switch
		{
			true =>
				new JwtConfig
				{
					SigningKey = Rnd.StringF.Get(32),
					EncryptingKey = Rnd.StringF.Get(64),
					Issuer = Rnd.Str,
					Audience = Rnd.Str
				},

			false =>
				new JwtConfig
				{
					SigningKey = Rnd.StringF.Get(32),
					Issuer = Rnd.Str,
					Audience = Rnd.Str
				}
		};

		var name = Rnd.Str;
		var identity = Substitute.For<IIdentity>();
		identity.IsAuthenticated.Returns(true);
		identity.Name.Returns(name);
		var principal = Substitute.For<ClaimsPrincipal>();
		principal.Identity.Returns(identity);

		var token = JwtF.CreateToken(
			config,
			principal,
			notBefore,
			expires
		).Unwrap();

		return (config, token, name);
	}

	[Fact]
	public void Not_Valid_Yet_Returns_None_With_TokenIsNotValidYetMsg()
	{
		// Arrange
		var (config, token, _) = GetToken(false, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(1));

		// Act
		var result = JwtF.ValidateToken(config, token);

		// Assert
		result.AssertFailure("Security token is not valid yet.");
	}

	[Fact]
	public void Expired_Returns_None_With_TokenHasExpiredMsg()
	{
		// Arrange
		var (config, token, _) = GetToken(false, DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddMinutes(-30));

		// Act
		var result = JwtF.ValidateToken(config, token);

		// Assert
		result.AssertFailure("Token has expired.");
	}

	[Fact]
	public void Valid_Token_Without_Encryption_Returns_Principal()
	{
		// Arrange
		var (config, token, name) = GetToken(false, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

		// Act
		var result = JwtF.ValidateToken(config, token);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(name, ok.Identity?.Name);
	}

	[Fact]
	public void Valid_Token_With_Encryption_Returns_Principal()
	{
		// Arrange
		var (config, token, name) = GetToken(true, DateTime.UtcNow, DateTime.UtcNow.AddHours(1));

		// Act
		var result = JwtF.ValidateToken(config, token);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(name, ok.Identity?.Name);
	}
}
