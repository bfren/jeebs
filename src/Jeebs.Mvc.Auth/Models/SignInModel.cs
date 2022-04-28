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
}
