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
			return R.Fail("Security token is not valid yet.")
				.Ctx(nameof(JwtF), nameof(ValidateToken));
		}
		catch (Exception e) when (e.Message.Contains("IDX10223"))
		{
			return R.Fail(e).Msg("Token has expired.")
				.Ctx(nameof(JwtF), nameof(ValidateToken));
		}
		catch (Exception e)
		{
			return R.Fail(e).Msg("Error validating token.")
				.Ctx(nameof(JwtF), nameof(ValidateToken));
		}
	}
}
