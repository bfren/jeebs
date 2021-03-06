// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Text;
using Jeebs.Config;
using Jm.Authentication.JwtConfigExtensions;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth
{
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
		public static Option<SecurityKey> GetEncryptingKey(this JwtConfig @this) =>
			@this.EncryptingKey switch
			{
				string key =>
					new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

				_ =>
					Option.None<SecurityKey>().AddReason<NullEncryptingKeyMsg>()
			};
	}
}
