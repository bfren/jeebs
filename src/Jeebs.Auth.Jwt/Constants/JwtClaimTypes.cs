// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Jwt.Constants;

/// <summary>
/// JWT claim types.
/// </summary>
public static class JwtClaimTypes
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
