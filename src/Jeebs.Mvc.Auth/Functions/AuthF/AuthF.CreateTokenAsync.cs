// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Auth.Jwt;
using Jeebs.Logging;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Create authentication token.
	/// </summary>
	/// <param name="jwt">IAuthJwtProvider.</param>
	/// <param name="user">ClaimsPrincipal.</param>
	/// <param name="log">ILog.</param>
	/// <returns>Auth operation result.</returns>
	public static async Task<AuthOp> CreateTokenAsync(IAuthJwtProvider jwt, ClaimsPrincipal user, ILog log) =>
		jwt.CreateToken(user)
			.Audit(fFail: log.Failure)
			.Match<JsonWebToken, AuthOp>(
				fOk: x => new AuthOp.SignedIn(x),
				fFail: _ => new AuthOp.TryAgain()
			);
}
