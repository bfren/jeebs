// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
