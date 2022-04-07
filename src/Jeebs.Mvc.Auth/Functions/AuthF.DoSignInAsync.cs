// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignInAsync(SignInArgs)"/>
	/// </summary>
	/// <param name="Model"></param>
	/// <param name="AddClaims"></param>
	/// <param name="AddErrorAlert"></param>
	/// <param name="Auth"></param>
	/// <param name="GetRedirect"></param>
	/// <param name="Log"></param>
	/// <param name="SignInAsync"></param>
	/// <param name="GetSignInFormPage"></param>
	/// <param name="Url"></param>
	public sealed record class SignInArgs(
		SignInModel Model,
		GetClaims? AddClaims,
		Action<string> AddErrorAlert,
		IAuthDataProvider Auth,
		Func<string, RedirectResult> GetRedirect,
		ILog Log,
		Func<string?, ClaimsPrincipal, AuthenticationProperties, Task> SignInAsync,
		Func<string?, IActionResult> GetSignInFormPage,
		IUrlHelper Url
	);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes
	/// </summary>
	/// <param name="v"></param>
	public static async Task<IActionResult> DoSignInAsync(SignInArgs v)
	{
		// Validate user
		var validateResult = await ValidateUserAsync(v.Auth, v.Model).AuditAsync(none: v.Log.Msg).ConfigureAwait(false);

		// Perform sign in
		if (validateResult.IsSome(out var user))
		{
			// Get user principal
			v.Log.Dbg("User {UserId} validated.", user.Id.Value);
			var principal = await GetPrincipal(user, v.Model.Password, v.AddClaims).ConfigureAwait(false);

			// Update last sign in
			_ = await UpdateUserLastSignInAsync(v.Auth, user.Id, v.Log).ConfigureAwait(false);

			// Sign in using cookie authentication scheme
			await v.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				principal,
				new AuthenticationProperties
				{
					ExpiresUtc = DateTime.UtcNow.AddDays(28),
					IsPersistent = v.Model.RememberMe,
					AllowRefresh = false,
					RedirectUri = v.Model.ReturnUrl
				}
			).ConfigureAwait(false);

			// Redirect to return url
			return v.GetRedirect(GetReturnUrl(v.Url, v.Model.ReturnUrl));
		}

		// Log error and add alert for user
		v.Log.Err("Unknown username or password: {Email}.", v.Model.Email);
		v.AddErrorAlert("Unknown username or password.");

		// Return to sign in page
		return v.GetSignInFormPage(v.Model.ReturnUrl);
	}
}
