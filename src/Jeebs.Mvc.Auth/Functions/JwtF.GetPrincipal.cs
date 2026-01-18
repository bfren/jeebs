// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Retrieve the user from the supplied token.
	/// </summary>
	/// <param name="auth">IJwtAuthProvider</param>
	/// <param name="token">Token value</param>
	internal static Maybe<ClaimsPrincipal> GetPrincipal(IAuthJwtProvider auth, string token) =>
		auth.ValidateToken(token);
}
