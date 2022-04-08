// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Auth.Models;

/// <summary>
/// Sign In Model
/// </summary>
public sealed record class SignInModel
{
	/// <summary>
	/// Email address
	/// </summary>
	public string Email { get; init; } = string.Empty;

	/// <summary>
	/// Password
	/// </summary>
	public string Password { get; init; } = string.Empty;

	/// <summary>
	/// Remember Me
	/// </summary>
	public bool RememberMe { get; init; }

	/// <summary>
	/// Return URL (after successful sign in)
	/// </summary>
	public string? ReturnUrl { get; init; }

	/// <summary>
	/// Create empty model
	/// </summary>
	/// <param name="returnUrl">[Optional] Return URL (after successful sign in)</param>
	public static SignInModel Empty(string? returnUrl) =>
		new() { ReturnUrl = returnUrl };
}
