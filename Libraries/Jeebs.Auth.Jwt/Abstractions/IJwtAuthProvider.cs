// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Security.Principal;

namespace Jeebs.Auth
{
	/// <summary>
	/// JSON Web Tokens authentication provider interface
	/// </summary>
	public interface IJwtAuthProvider
	{
		/// <summary>
		/// Generate a new JSON Web Token for the specified user
		/// </summary>
		/// <param name="principal">IPrincipal</param>
		Option<string> CreateToken(IPrincipal principal);

		/// <summary>
		/// Validate a JSON Web Token
		/// </summary>
		/// <param name="token">JSON Web Token</param>
		Option<IPrincipal> ValidateToken(string token);
	}
}
