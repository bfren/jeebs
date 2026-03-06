// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Config.Web.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt.Functions;

public static partial class JwtF
{
	/// <summary>
	/// <para>Generate a new JSON Web Token for the specified user</para>
	/// <para>See <see cref="JwtSecurity"/> for default signing and encrypting algorithms</para>
	/// </summary>
	/// <param name="config">JwtConfig</param>
	/// <param name="principal">ClaimsPrincipal</param>
	/// <returns>JsonWebToken.</returns>
	public static Result<JsonWebToken> CreateToken(JwtConfig config, ClaimsPrincipal principal) =>
		CreateToken(config, principal, DateTime.UtcNow, DateTime.UtcNow.AddHours(config.ValidForHours));

	/// <summary>
	/// <para>Generate a new JSON Web Token for the specified user</para>
	/// <para>See <see cref="JwtSecurity"/> for default signing and encrypting algorithms</para>
	/// </summary>
	/// <param name="config">JwtConfig</param>
	/// <param name="principal">ClaimsPrincipal</param>
	/// <param name="notBefore">The earliest date / time from which this token is valid</param>
	/// <param name="expires">The latest date / time before which this token is valid</param>
	/// <returns>JsonWebToken.</returns>
	internal static Result<JsonWebToken> CreateToken(
		JwtConfig config,
		ClaimsPrincipal principal,
		DateTime notBefore,
		DateTime expires
	)
	{
		// Ensure there is a current user
		if (principal.Identity is null)
		{
			return R.Fail("User cannot be null.")
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}

		// Ensure the current user is authenticated
		var identity = principal.Identity;
		if (!identity.IsAuthenticated)
		{
			return R.Fail("User is not authenticated.")
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}

		// Ensure the JwtConfig is valid
		if (!config.IsValid)
		{
			return R.Fail("JWT configuration is invalid.")
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}

		// Ensure the signing key is a valid length
		if (config.SigningKey.Length < JwtSecurity.SigningKeyBytes)
		{
			return R.Fail("JWT signing key is not long enough ({Length}) - should be {Minimum}.", config.SigningKey.Length, JwtSecurity.SigningKeyBytes)
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}

		// Ensure the encrypting key is a valid length
		if (config.EncryptingKey is string key && key.Length < JwtSecurity.EncryptingKeyBytes)
		{
			return R.Fail("JWT encrypting key is not long enough ({Length}) - should be {Minimum}.", key.Length, JwtSecurity.EncryptingKeyBytes)
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}

		try
		{
			// Create token values
			var descriptor = new SecurityTokenDescriptor
			{
				Issuer = config.Issuer,
				Audience = config.Audience ?? config.Issuer,
				Subject = new ClaimsIdentity(identity),
				NotBefore = notBefore,
				Expires = expires,
				IssuedAt = DateTime.UtcNow,
				SigningCredentials = new SigningCredentials(config.GetSigningKey(), JwtSecurity.SigningAlgorithm)
			};

			_ = config.GetEncryptingKey().IfSome(encryptingKey2 =>
				  descriptor.EncryptingCredentials = new EncryptingCredentials(
					  encryptingKey2,
					  JwtSecurity.KeyWrapAlgorithm,
					  JwtSecurity.EncryptingAlgorithm
				  )
			);

			// Create handler to create and write token
			var handler = new JwtSecurityTokenHandler();
			var token = handler.CreateJwtSecurityToken(descriptor);
			return JsonWebToken.Wrap(handler.WriteToken(token));
		}
		catch (ArgumentOutOfRangeException e) when (e.Message.Contains("IDX10653"))
		{
			return R.Fail("Key is not long enough.")
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}
		catch (Exception e)
		{
			return R.Fail(e).Msg("Error creating JWT security token.")
				.Ctx(nameof(JwtF), nameof(CreateToken));
		}
	}
}
