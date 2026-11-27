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
			return UserIsNull();
		}

		// Ensure the current user is authenticated
		var identity = principal.Identity;
		if (!identity.IsAuthenticated)
		{
			return UserNotAuthenticated();
		}

		// Ensure the JwtConfig is valid
		if (!config.IsValid)
		{
			return ConfigurationInvalid();
		}

		// Ensure the signing key is a valid length
		if (config.SigningKey.Length < JwtSecurity.SigningKeyBytes)
		{
			return SigningKeyTooShort(config.SigningKey.Length, JwtSecurity.SigningKeyBytes);
		}

		// Ensure the encrypting key is a valid length
		if (config.EncryptingKey is string key && key.Length < JwtSecurity.EncryptingKeyBytes)
		{
			return EncryptingKeyTooShort(key.Length, JwtSecurity.EncryptingKeyBytes);
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
			return KeyNotLongEnough();
		}
		catch (Exception e)
		{
			return ErrorCreatingToken(e);
		}
	}

	[Failure("User cannot be null.")]
	private static partial Failure UserIsNull();

	[Failure("User is not authenticated.")]
	private static partial Failure UserNotAuthenticated();

	[Failure("JWT configuration is invalid.")]
	private static partial Failure ConfigurationInvalid();

	[Failure("JWT signing key is not long enough ({Length}) - should be {Minimum}.")]
	private static partial Failure SigningKeyTooShort(int length, int minimum);

	[Failure("JWT encrypting key is not long enough ({Length}) - should be {Minimum}.")]
	private static partial Failure EncryptingKeyTooShort(int length, int minimum);

	[Failure("Key is not long enough.")]
	private static partial Failure KeyNotLongEnough();

	[Failure("Error creating JWT security token.")]
	private static partial Failure ErrorCreatingToken(Exception e);
}
