// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text;
using Jeebs.Config;
using Microsoft.IdentityModel.Tokens;
using static F.OptionF;
using Msg = Jeebs.Auth.JwtConfigExtensionsMsg;

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
					None<SecurityKey, Msg.NullEncryptingKeyMsg>()
			};
	}

	namespace JwtConfigExtensionsMsg
	{
		/// <summary>The Encrypting Key is null</summary>
		public sealed record NullEncryptingKeyMsg : IMsg { }
	}
}
