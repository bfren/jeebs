// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Auth
{
	/// <summary>
	/// JWT claim types
	/// </summary>
	public static class JwtClaimTypes
	{
		/// <summary>
		/// User ID claim type
		/// </summary>
		public const string UserId = "jeebs:user:id";

		/// <summary>
		/// IsSuper claim type
		/// </summary>
		public const string IsSuper = "jeebs:user:isSuper";
	}
}
