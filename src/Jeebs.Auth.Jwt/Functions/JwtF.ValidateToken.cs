// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Messages;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Validate tokens.
	/// </summary>
	/// <param name="config">JwtConfig.</param>
	/// <param name="token">Token value.</param>
	public static Maybe<ClaimsPrincipal> ValidateToken(JwtConfig config, string token)
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
			return handler.ValidateToken(token, parameters, out var validatedToken);
		}
		catch (SecurityTokenNotYetValidException)
		{
			return F.None<ClaimsPrincipal, M.TokenIsNotValidYetMsg>();
		}
		catch (Exception e) when (e.Message.Contains("IDX10223"))
		{
			return F.None<ClaimsPrincipal, M.TokenHasExpiredMsg>();
		}
		catch (Exception e)
		{
			return F.None<ClaimsPrincipal, M.ValidatingTokenExceptionMsg>(e);
		}
	}

	public static partial class M
	{
		/// <summary>The token has expired</summary>
		public sealed record class TokenHasExpiredMsg : Msg;

		/// <summary>The token is not valid yet</summary>
		public sealed record class TokenIsNotValidYetMsg : Msg;

		/// <summary>Exception while validating token</summary>
		/// <param name="Value">Exception.</param>
		public sealed record class ValidatingTokenExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
