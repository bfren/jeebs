using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Mvc.Auth.Models
{
	/// <summary>
	/// Sign In Model
	/// </summary>
	/// <param name="Email">Email address</param>
	/// <param name="Password">Password</param>
	/// <param name="RememberMe">Remember Me</param>
	/// <param name="ReturnUrl">Return URL (after successful sign in)</param>
	public sealed record SignInModel(string Email, string Password, bool RememberMe, string? ReturnUrl)
	{
		/// <summary>
		/// Create empty model
		/// </summary>
		/// <param name="returnUrl">[Optional] Return URL (after successful sign in)</param>
		public SignInModel(string? returnUrl) : this(string.Empty, string.Empty, false, returnUrl) { }
	}
}
