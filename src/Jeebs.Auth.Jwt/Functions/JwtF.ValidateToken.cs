// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs.Config.Web.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Validate a JSON Web Token.
	/// </summary>
	/// <param name="config">JwtConfig.</param>
	/// <param name="token">Token value.</param>
	/// <returns>User info (if token is valid).</returns>
	public static Result<ClaimsPrincipal> ValidateToken(JwtConfig config, JsonWebToken token)
	{
		try
		{
			// Create validation parameters
			var parameters = new TokenValidationParameters
			{
				RequireExpirationTime = true,
				ValidIssuer = config.Issuer,
				ValidAudience = config.Audience ?? config.Issuer,
				IssuerSigningKey = config.GetSigningKey()
			};

			_ = config.GetEncryptingKey().IfSome(encryptingKey => parameters.TokenDecryptionKey = encryptingKey);

			// Create handler to validate token
			var handler = new JwtSecurityTokenHandler();

			// Validate token and return principal
			return handler.ValidateToken(token.Value, parameters, out var validatedToken);
		}
		catch (SecurityTokenNotYetValidException)
		{
			return TokenNotValidYet();
		}
		catch (SecurityTokenExpiredException)
		{
			return TokenExpired();
		}
		catch (Exception e)
		{
			return ErrorValidatingToken() with { Exception = e };
		}
	}

	[Failure("Security token is not valid yet.")]
	private static partial Failure TokenNotValidYet();

	[Failure("Token has expired.")]
	private static partial Failure TokenExpired();

	[Failure("Error validating token.", Wrap.Logging.LogLevel.Error)]
	private static partial Failure ErrorValidatingToken();
}
