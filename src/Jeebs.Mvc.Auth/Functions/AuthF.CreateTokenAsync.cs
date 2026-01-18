// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
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
	public static Task<AuthResult> CreateTokenAsync(IAuthJwtProvider jwt, ClaimsPrincipal user, ILog log)
	{
		var token = jwt
			.CreateToken(user)
			.Audit(none: log.Msg)
			.Switch<AuthResult>(
				some: x => new AuthResult.SignedIn(x),
				none: _ => new AuthResult.TryAgain()
			);

		return Task.FromResult(token);
	}
}
