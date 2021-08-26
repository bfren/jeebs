// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Mvc.Auth.Models
{
	/// <summary>
	/// Sign In Model
	/// </summary>
	/// <param name="Email">Email address</param>
	/// <param name="Password">Password</param>
	/// <param name="RememberMe">Remember Me</param>
	/// <param name="ReturnUrl">Return URL (after successful sign in)</param>
	public sealed record class SignInModel(string Email, string Password, bool RememberMe, string? ReturnUrl)
	{
		/// <summary>
		/// Create empty model
		/// </summary>
		/// <param name="returnUrl">[Optional] Return URL (after successful sign in)</param>
		public static SignInModel Empty(string? returnUrl) =>
			new(string.Empty, string.Empty, false, returnUrl);
	}
}
