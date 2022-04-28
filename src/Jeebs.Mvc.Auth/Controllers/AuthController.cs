// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static Jeebs.Mvc.Auth.Functions.AuthF;

namespace Jeebs.Mvc.Auth.Controllers;

/// <inheritdoc cref="AuthControllerBase"/>
public abstract class AuthController : AuthControllerBase
{
	/// <summary>
	/// AuthDataProvider
	/// </summary>
	protected new AuthDataProvider Auth { get; private init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="auth">AuthDataProvider</param>
	/// <param name="log">ILog</param>
	protected AuthController(AuthDataProvider auth, Logging.ILog log) : base(auth, log) =>
		Auth = auth;
}

/// <summary>
/// Implement this controller to add support for user authentication
/// </summary>
public abstract class AuthControllerBase : Mvc.Controllers.Controller
{
	/// <summary>
	/// IAuthDataProvider
	/// </summary>
	protected IAuthDataProvider Auth { get; private init; }

	/// <summary>
	/// Get application-specific claims for an authenticated user
	/// </summary>
	protected virtual GetClaims? GetClaims { get; }

	/// <summary>
	/// Redirect here after a successful sign in
	/// </summary>
	protected virtual Func<string?> SignInRedirect { get; init; }

	/// <summary>
	/// Redirect here after a successful sign out
	/// </summary>
	protected virtual Func<string?> SignOutRedirect { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="auth">IAuthDataProvider</param>
	/// <param name="log">ILog</param>
	protected AuthControllerBase(IAuthDataProvider auth, Logging.ILog log) : base(log)
	{
		Auth = auth;
		SignInRedirect = () => Url.Action("Index", "Home");
		SignOutRedirect = () => Url.Action("Auth", "SignOut");
	}

	/// <summary>
	/// Display sign in page
	/// </summary>
	public IActionResult SignIn() =>
		View(new SignInModel());

	/// <summary>
	/// Check TOTP requirement or perform sign in
	/// </summary>
	/// <param name="model">SignInModel</param>
	[HttpPost, ValidateAntiForgeryToken]
	public virtual async Task<IActionResult> SignIn(SignInModel model)
	{
		// Do sign in
		var result = await DoSignInAsync(new(
			Model: model,
			Auth: Auth,
			Log: Log,
			Url: Url,
			AddErrorAlert: TempData.AddErrorAlert,
			GetClaims: GetClaims,
			RedirectUrl: SignInRedirect,
			SignInAsync: HttpContext.SignInAsync,
			ValidateUserAsync: ValidateUserAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedIn =>
				result,

			AuthResult.TryAgain =>
				SignIn(),

			_ =>
				Denied(Request.GetDisplayUrl())
		};
	}

	/// <summary>
	/// Perform sign out
	/// </summary>
	public new async Task<IActionResult> SignOut()
	{
		// Do sign out
		var result = await DoSignOutAsync(new(
			AddInfoAlert: TempData.AddInfoAlert,
			RedirectUrl: SignOutRedirect,
			SignOutAsync: HttpContext.SignOutAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedOut =>
				result,

			_ =>
				Denied(Request.GetDisplayUrl())
		};
	}

	/// <summary>
	/// Show access denied page
	/// </summary>
	/// <param name="accessUrl">URL that was accessed</param>
	public IActionResult Denied(string? accessUrl) =>
		View(new DeniedModel(accessUrl));
}
