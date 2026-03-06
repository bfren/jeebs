// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignOutAsync(SignOutArgs)"/>.
	/// </summary>
	/// <param name="AddInfoAlert">Function to add an info alert.</param>
	/// <param name="RedirectUrl">Function to return the Redirect URL (if set).</param>
	/// <param name="SignOutAsync">Function to perform the signing out.</param>
	public sealed record class SignOutArgs(
		Action<string> AddInfoAlert,
		Func<string?> RedirectUrl,
		Func<Task> SignOutAsync
	);

	/// <summary>
	/// Sign a user out of the session.
	/// </summary>
	/// <param name="v">SignOutArgs.</param>
	public static async Task<AuthOp> DoSignOutAsync(SignOutArgs v)
	{
		// Sign out
		await v.SignOutAsync();

		// Show a friendly message to the user
		v.AddInfoAlert("Goodbye!");

		// Redirect to sign in page
		return new AuthOp.SignedOut(v.RedirectUrl());
	}
}
