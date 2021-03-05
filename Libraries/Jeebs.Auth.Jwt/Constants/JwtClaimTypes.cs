using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
