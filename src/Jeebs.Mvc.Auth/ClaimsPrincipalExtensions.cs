// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Security.Claims;
using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Jwt.Constants;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// <see cref="ClaimsPrincipal"/> extension methods
/// </summary>
public static class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns a specified claim for the current user
	/// </summary>
	/// <param name="this">ClaimsPrincipal</param>
	/// <param name="type">Claim type</param>
	public static Maybe<string> GetClaim(this ClaimsPrincipal @this, string type)
	{
		if (@this.Identity?.IsAuthenticated != true)
		{
			return M.None;
		}

		return @this.Claims
			.Where(c => c.Type == type)
			.SingleOrNone()
			.Map(c => c.Value);
	}
	/// <summary>
	/// Returns the ID of the current user
	/// </summary>
	/// <param name="this">CLaimsPrincipal</param>
	public static Maybe<AuthUserId> GetUserId(this ClaimsPrincipal @this) =>
		@this.GetClaim(JwtClaimTypes.UserId)
			.Bind(M.ParseInt64)
			.Map(AuthUserId.Wrap);

	/// <summary>
	/// Returns whether or not the current user has the specified claim
	/// </summary>
	/// <param name="this">ClaimsPrincipal</param>
	/// <param name="type">Claim type</param>
	public static bool HasClaim(this ClaimsPrincipal @this, string type) =>
		@this.GetClaim(type).IsSome;

	/// <summary>
	/// Returns whether or not the current user is a Super user
	/// </summary>
	/// <param name="this">CLaimsPrincipal</param>
	public static bool IsSuper(this ClaimsPrincipal @this) =>
		@this.HasClaim(JwtClaimTypes.IsSuper);
}
