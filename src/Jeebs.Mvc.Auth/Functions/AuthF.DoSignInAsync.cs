// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignInAsync(SignInArgs)"/>
	/// </summary>
	/// <param name="Model"></param>
	/// <param name="AddClaims"></param>
	/// <param name="Auth"></param>
	/// <param name="Context"></param>
	/// <param name="Log"></param>
	/// <param name="RedirectTo"></param>
	/// <param name="SignInFormPage"></param>
	/// <param name="TempData"></param>
	/// <param name="Url"></param>
	public sealed record class SignInArgs(
		SignInModel Model,
		GetClaims? AddClaims,
		IAuthDataProvider Auth,
		HttpContext Context,
		ILog Log,
		Func<string, RedirectResult> RedirectTo,
		Func<string?, IActionResult> SignInFormPage,
		ITempDataDictionary TempData,
		IUrlHelper Url
	);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes
	/// </summary>
	/// <param name="v"></param>
	public static async Task<IActionResult> DoSignInAsync(SignInArgs v)
	{
		// Validate user
		var validatedUser = from _ in v.Auth.ValidateUserAsync<AuthUserModel>(v.Model.Email, v.Model.Password)
							from user in v.Auth.RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(v.Model.Email)
							select user;

		await foreach (var user in validatedUser)
		{
			// Get user principal
			v.Log.Dbg("User validated.");
			var principal = await GetPrincipal(user, v.Model.Password, v.AddClaims).ConfigureAwait(false);

			// Update last sign in
			var updated = await v.Auth.User.UpdateLastSignInAsync(user.Id).ConfigureAwait(false);
			_ = updated.Audit(none: v.Log.Msg);

			// Add SignIn to HttpContext using Cookie scheme
			await v.Context.SignInAsync(
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

			// Redirect to return url (or Auth/Index)
			return v.RedirectTo(GetReturnUrl(v.Url, v.Model.ReturnUrl));
		}

		// Log error and add alert for user
		v.Log.Dbg("Unknown username or password: {Email}.", v.Model.Email);
		v.TempData.AddErrorAlert("Unknown username or password.");

		// Return to sign in page
		return v.SignInFormPage(v.Model.ReturnUrl);
	}
}
