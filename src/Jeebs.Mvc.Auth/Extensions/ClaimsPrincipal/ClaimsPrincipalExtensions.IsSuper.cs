// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;
using CT = Jeebs.Auth.Jwt.Constants.ClaimTypes;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// <see cref="ClaimsPrincipal"/> extension methods.
/// </summary>
public static partial class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Returns whether or not the current user is a Super user.
	/// </summary>
	/// <param name="this">CLaimsPrincipal.</param>
	/// <returns>True if the current user has Super permissions.</returns>
	public static bool IsSuper(this ClaimsPrincipal @this) =>
		@this.HasClaim(CT.IsSuper);
}
