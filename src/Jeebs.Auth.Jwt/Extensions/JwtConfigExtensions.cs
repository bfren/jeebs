// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Config;
using Microsoft.IdentityModel.Tokens;
using static F.MaybeF;

namespace Jeebs.Auth;

/// <summary>
/// JwtConfig extension methods
/// </summary>
public static class JwtConfigExtensions
{
	/// <summary>
	/// Get Signing Key
	/// </summary>
	public static SecurityKey GetSigningKey(this JwtConfig @this) =>
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@this.SigningKey));

	/// <summary>
	/// Get Encrypting Key
	/// </summary>
	public static Maybe<SecurityKey> GetEncryptingKey(this JwtConfig @this) =>
		@this.EncryptingKey switch
		{
			string key =>
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

			_ =>
				None<SecurityKey, M.NullEncryptingKeyMsg>()
		};

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>The Encrypting Key is null</summary>
		public sealed record class NullEncryptingKeyMsg : Msg;
	}
}
