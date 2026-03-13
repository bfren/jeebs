// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using Jeebs.Config.Web.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Auth.Jwt;

public static partial class JwtConfigExtensions
{
	/// <summary>
	/// Get Signing Key.
	/// </summary>
	/// <param name="this">JwtConfig.</param>
	/// <returns>JWT Signing Key.</returns>
	public static SecurityKey GetSigningKey(this JwtConfig @this) =>
		new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@this.SigningKey));
}
