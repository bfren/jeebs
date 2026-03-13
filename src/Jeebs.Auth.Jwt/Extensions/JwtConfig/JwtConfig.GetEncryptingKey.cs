// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Config.Web.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt;

public static partial class JwtConfigExtensions
{
	/// <summary>
	/// Get Encrypting Key.
	/// </summary>
	/// <param name="this">JwtConfig.</param>
	/// <returns>JWT Encrypting Key.</returns>
	public static Maybe<SecurityKey> GetEncryptingKey(this JwtConfig @this) =>
		@this.EncryptingKey switch
		{
			string key =>
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

			_ =>
				M.None
		};
}
