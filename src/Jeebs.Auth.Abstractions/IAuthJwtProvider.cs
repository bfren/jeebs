// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Security.Claims;

namespace Jeebs.Auth
{
	/// <summary>
	/// JSON Web Tokens authentication provider interface
	/// </summary>
	public interface IAuthJwtProvider
	{
		/// <summary>
		/// Generate a new JSON Web Token for the specified user
		/// </summary>
		/// <param name="principal">IPrincipal</param>
		Option<string> CreateToken(ClaimsPrincipal principal);

		/// <summary>
		/// Validate a JSON Web Token
		/// </summary>
		/// <param name="token">JSON Web Token</param>
		Option<ClaimsPrincipal> ValidateToken(string token);
	}
}
