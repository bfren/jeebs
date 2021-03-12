// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs.Auth;
using Jeebs.Auth.Constants;
using Jeebs.Config;
using Jm.Functions.JwtF.CreateToken;
using Microsoft.IdentityModel.Tokens;

namespace JeebsF
{
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
		public static Option<string> CreateToken(JwtConfig config, ClaimsPrincipal principal) =>
			CreateToken(config, principal, DateTime.UtcNow, DateTime.UtcNow.AddHours(config.ValidForHours));

		/// <summary>
		/// <para>Generate a new JSON Web Token for the specified user</para>
		/// <para>See <see cref="JwtSecurity"/> for default signing and encrypting algorithms</para>
		/// </summary>
		/// <param name="config">JwtConfig</param>
		/// <param name="principal">ClaimsPrincipal</param>
		/// <param name="notBefore">The earliest date / time from which this token is valid</param>
		/// <param name="expires">The latest date / time before which this token is valid</param>
		internal static Option<string> CreateToken(
			JwtConfig config,
			ClaimsPrincipal principal,
			DateTime notBefore,
			DateTime expires
		)
		{
			// Ensure there is a current user
			if (principal.Identity == null)
			{
				return OptionF.None<string>(new NullIdentityMsg());
			}

			// Ensure the current user is authenticated
			var identity = principal.Identity;
			if (!identity.IsAuthenticated)
			{
				return OptionF.None<string>(new IdentityNotAuthenticatedMsg());
			}

			// Ensure the JwtConfig is valid
			if (!config.IsValid)
			{
				return OptionF.None<string>(new JwtConfigInvalidMsg());
			}

			// Ensure the signing key is a valid length
			if (config.SigningKey.Length < JwtSecurity.SigningKeyBytes)
			{
				return OptionF.None<string>(new SigningKeyNotLongEnoughMsg());
			}

			// Ensure the encrypting key is a valid length
			if (config.EncryptingKey is string key && key.Length < JwtSecurity.EncryptingKeyBytes)
			{
				return OptionF.None<string>(new EncryptingKeyNotLongEnoughMsg());
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

				if (config.GetEncryptingKey() is Some<SecurityKey> encryptingKey2)
				{
					descriptor.EncryptingCredentials = new EncryptingCredentials(
						encryptingKey2.Value,
						JwtSecurity.KeyWrapAlgorithm,
						JwtSecurity.EncryptingAlgorithm
					);
				}

				// Create handler to create and write token
				var handler = new JwtSecurityTokenHandler();
				var token = handler.CreateJwtSecurityToken(descriptor);
				return handler.WriteToken(token);
			}
			catch (ArgumentOutOfRangeException e) when (e.Message.Contains("IDX10653"))
			{
				return OptionF.None<string>(new KeyNotLongEnoughMsg());
			}
			catch (Exception e)
			{
				return OptionF.None<string>(new ErrorCreatingJwtSecurityTokenMsg(e));
			}
		}
	}
}
