// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Messages;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt.Functions;

/// <summary>
/// JSON Web Tokens functions
/// </summary>
public static partial class JwtF
{
	/// <summary>
	/// <para>Generate a new JSON Web Token for the specified user</para>
	/// <para>See <see cref="JwtSecurity"/> for default signing and encrypting algorithms</para>
	/// </summary>
	/// <param name="config">JwtConfig</param>
	/// <param name="principal">ClaimsPrincipal</param>
	public static Maybe<string> CreateToken(JwtConfig config, ClaimsPrincipal principal) =>
		CreateToken(config, principal, DateTime.UtcNow, DateTime.UtcNow.AddHours(config.ValidForHours));

	/// <summary>
	/// <para>Generate a new JSON Web Token for the specified user</para>
	/// <para>See <see cref="JwtSecurity"/> for default signing and encrypting algorithms</para>
	/// </summary>
	/// <param name="config">JwtConfig</param>
	/// <param name="principal">ClaimsPrincipal</param>
	/// <param name="notBefore">The earliest date / time from which this token is valid</param>
	/// <param name="expires">The latest date / time before which this token is valid</param>
	internal static Maybe<string> CreateToken(
		JwtConfig config,
		ClaimsPrincipal principal,
		DateTime notBefore,
		DateTime expires
	)
	{
		// Ensure there is a current user
		if (principal.Identity is null)
		{
			return F.None<string, M.NullIdentityMsg>();
		}

		// Ensure the current user is authenticated
		var identity = principal.Identity;
		if (!identity.IsAuthenticated)
		{
			return F.None<string, M.IdentityNotAuthenticatedMsg>();
		}

		// Ensure the JwtConfig is valid
		if (!config.IsValid)
		{
			return F.None<string, M.ConfigInvalidMsg>();
		}

		// Ensure the signing key is a valid length
		if (config.SigningKey.Length < JwtSecurity.SigningKeyBytes)
		{
			return F.None<string, M.SigningKeyNotLongEnoughMsg>();
		}

		// Ensure the encrypting key is a valid length
		if (config.EncryptingKey is string key && key.Length < JwtSecurity.EncryptingKeyBytes)
		{
			return F.None<string, M.EncryptingKeyNotLongEnoughMsg>();
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
			return handler.WriteToken(token);
		}
		catch (ArgumentOutOfRangeException e) when (e.Message.Contains("IDX10653"))
		{
			return F.None<string, M.KeyNotLongEnoughMsg>();
		}
		catch (Exception e)
		{
			return F.None<string>(new M.CreatingJwtSecurityTokenExceptionMsg(e));
		}
	}

	public static partial class M
	{
		/// <summary>JwtConfig invalid</summary>
		public sealed record class ConfigInvalidMsg : Msg;

		/// <summary>Exception when creating JwtSecurityToken</summary>
		/// <param name="Value">Exception</param>
		public sealed record class CreatingJwtSecurityTokenExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>The Encrypting Key is not long enough</summary>
		public sealed record class EncryptingKeyNotLongEnoughMsg : Msg;

		/// <summary>The User's Identity is not authenticated</summary>
		public sealed record class IdentityNotAuthenticatedMsg : Msg;

		/// <summary>One of the Signing / Encrypting keys is not long enough</summary>
		public sealed record class KeyNotLongEnoughMsg : Msg;

		/// <summary>The Principal's Identity is null</summary>
		public sealed record class NullIdentityMsg : Msg;

		/// <summary>The Signing Key is not long enough</summary>
		public sealed record class SigningKeyNotLongEnoughMsg : Msg;
	}
}
