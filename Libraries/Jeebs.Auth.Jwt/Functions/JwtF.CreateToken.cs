// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Constants;
using Jeebs.Config;
using Microsoft.IdentityModel.Tokens;
using static F.OptionF;

namespace F
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
				return None<string, Msg.NullIdentityMsg>();
			}

			// Ensure the current user is authenticated
			var identity = principal.Identity;
			if (!identity.IsAuthenticated)
			{
				return None<string, Msg.IdentityNotAuthenticatedMsg>();
			}

			// Ensure the JwtConfig is valid
			if (!config.IsValid)
			{
				return None<string, Msg.ConfigInvalidMsg>();
			}

			// Ensure the signing key is a valid length
			if (config.SigningKey.Length < JwtSecurity.SigningKeyBytes)
			{
				return None<string, Msg.SigningKeyNotLongEnoughMsg>();
			}

			// Ensure the encrypting key is a valid length
			if (config.EncryptingKey is string key && key.Length < JwtSecurity.EncryptingKeyBytes)
			{
				return None<string, Msg.EncryptingKeyNotLongEnoughMsg>();
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
				return None<string, Msg.KeyNotLongEnoughMsg>();
			}
			catch (Exception e)
			{
				return None<string>(new Msg.CreatingJwtSecurityTokenExceptionMsg(e));
			}
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>JwtConfig invalid</summary>
			public sealed record ConfigInvalidMsg : IMsg { }

			/// <summary>Exception when creating JwtSecurityToken</summary>
			public sealed record CreatingJwtSecurityTokenExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>The Encrypting Key is not long enough</summary>
			public sealed record EncryptingKeyNotLongEnoughMsg : IMsg { }

			/// <summary>The User's Identity is not authenticated</summary>
			public sealed record IdentityNotAuthenticatedMsg : IMsg { }

			/// <summary>One of the Signing / Encrypting keys is not long enough</summary>
			public sealed record KeyNotLongEnoughMsg : IMsg { }

			/// <summary>The Principal's Identity is null</summary>
			public sealed record NullIdentityMsg : IMsg { }

			/// <summary>The Signing Key is not long enough</summary>
			public sealed record SigningKeyNotLongEnoughMsg : IMsg { }
		}
	}
}
