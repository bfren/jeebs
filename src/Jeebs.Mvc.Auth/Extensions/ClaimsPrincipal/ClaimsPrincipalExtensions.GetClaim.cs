// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Security.Claims;

namespace Jeebs.Mvc.Auth;

public static partial class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns a specified Claim value for the current user.
	/// </summary>
	/// <param name="this">ClaimsPrincipal.</param>
	/// <param name="claimType">Claim type.</param>
	/// <returns>Claim value or None if Claim not found.</returns>
	public static Maybe<string> GetClaim(this ClaimsPrincipal @this, string claimType)
	{
		if (@this.Identity?.IsAuthenticated != true)
		{
			return M.None;
		}

		return @this.Claims
			.Where(c => c.Type == claimType)
			.SingleOrNone()
			.Map(c => c.Value);
	}
}
