// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignOutAsync(SignOutArgs)"/>
	/// </summary>
	/// <param name="AddInfoAlert"></param>
	/// <param name="GetSignInFormPage"></param>
	/// <param name="SignOutAsync"></param>
	public sealed record class SignOutArgs(
		Action<string> AddInfoAlert,
		Func<IActionResult> GetSignInFormPage,
		Func<Task> SignOutAsync
	);

	/// <summary>
	/// Sign a user out of the session
	/// </summary>
	/// <param name="v"></param>
	public static async Task<IActionResult> DoSignOutAsync(SignOutArgs v)
	{
		// Sign out
		await v.SignOutAsync().ConfigureAwait(false);

		// Show a friendly message to the user
		v.AddInfoAlert("Goodbye!");

		// Redirect to sign in page
		return v.GetSignInFormPage();
	}
}
