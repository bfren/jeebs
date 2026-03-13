// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using Jeebs.Auth.Data.Ids;
using CT = Jeebs.Auth.Jwt.Constants.ClaimTypes;

namespace Jeebs.Mvc.Auth;

public static partial class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns the ID of the current user.
	/// </summary>
	/// <param name="this">CLaimsPrincipal.</param>
	/// <remarks>User ID or None if not logged in.</remarks>
	public static Maybe<AuthUserId> GetUserId(this ClaimsPrincipal @this) =>
		@this.GetClaim(CT.UserId)
			.Bind(M.ParseInt64)
			.Map(AuthUserId.Wrap);
}
