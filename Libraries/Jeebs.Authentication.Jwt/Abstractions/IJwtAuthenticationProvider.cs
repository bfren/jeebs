using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Authentication
{
	/// <summary>
	/// JSON Web Tokens authentication provider interface
	/// </summary>
	public interface IJwtAuthenticationProvider
	{
		/// <summary>
		/// Generate a new JSON Web Token for the specified user
		/// </summary>
		/// <param name="principal">IPrincipal</param>
		Option<string> CreateToken(IPrincipal principal);

		/// <summary>
		/// Validate a JSON Web Token
		/// </summary>
		/// <param name="token">JSON Web Token</param>
		Option<IPrincipal> ValidateToken(string token);
	}
}
