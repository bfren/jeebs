// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Jwt.Constants;

/// <summary>
/// Claim types for User Principals.
/// </summary>
public static class ClaimTypes
{
	/// <summary>
	/// User ID claim type.
	/// </summary>
	public static readonly string UserId = "jeebs:user:id";

	/// <summary>
	/// IsSuper claim type.
	/// </summary>
	public static readonly string IsSuper = "jeebs:user:isSuper";
}
