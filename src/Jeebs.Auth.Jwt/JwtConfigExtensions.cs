// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Config.Web.Auth.Jwt;
using Jeebs.Messages;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt;

/// <summary>
/// JwtConfig extension methods
/// </summary>
public static class JwtConfigExtensions
{
	/// <summary>
	/// Get Signing Key
	/// </summary>
	/// <param name="this">JwtConfig</param>
	public static SecurityKey GetSigningKey(this JwtConfig @this) =>
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@this.SigningKey));

	/// <summary>
	/// Get Encrypting Key
	/// </summary>
	/// <param name="this">JwtConfig</param>
	public static Maybe<SecurityKey> GetEncryptingKey(this JwtConfig @this) =>
		@this.EncryptingKey switch
		{
			string key =>
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

			_ =>
				F.None<SecurityKey, M.NullEncryptingKeyMsg>()
		};

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The Encrypting Key is null</summary>
		public sealed record class NullEncryptingKeyMsg : Msg;
	}
}
