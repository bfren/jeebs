// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignOutAsync(SignOutArgs)"/>
	/// </summary>
	/// <param name="Context"></param>
	/// <param name="SignInFormPage"></param>
	/// <param name="TempData"></param>
	public sealed record class SignOutArgs(
		HttpContext Context,
		Func<IActionResult> SignInFormPage,
		ITempDataDictionary TempData
	);

	/// <summary>
	/// Sign a user out of the session
	/// </summary>
	/// <param name="v"></param>
	public static async Task<IActionResult> DoSignOutAsync(SignOutArgs v)
	{
		// Sign out
		await v.Context.SignOutAsync().ConfigureAwait(false);

		// Show a friendly message to the user
		v.TempData.AddInfoAlert("Goodbye!");

		// Redirect to sign in page
		return v.SignInFormPage();
	}
}
