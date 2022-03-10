// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using MaybeF;

namespace Jeebs.Auth;

/// <summary>
/// JSON Web Tokens authentication provider interface
/// </summary>
public interface IAuthJwtProvider
{
	/// <summary>
	/// Generate a new JSON Web Token for the specified user
	/// </summary>
	/// <param name="principal">IPrincipal</param>
	Maybe<string> CreateToken(ClaimsPrincipal principal);

	/// <summary>
	/// Validate a JSON Web Token
	/// </summary>
	/// <param name="token">JSON Web Token</param>
	Maybe<ClaimsPrincipal> ValidateToken(string token);
}
