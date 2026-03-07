// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;

namespace Jeebs.Mvc.Auth;

public static partial class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns whether or not the current user has the specified Claim.
	/// </summary>
	/// <param name="this">ClaimsPrincipal.</param>
	/// <param name="claimType">Claim type.</param>
	/// <returns>Whether or not the current user has the specified Claim.</returns>
	public static bool HasClaim(this ClaimsPrincipal @this, string claimType) =>
		@this.GetClaim(claimType).IsSome;
}
