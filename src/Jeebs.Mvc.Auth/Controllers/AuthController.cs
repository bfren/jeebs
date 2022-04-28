// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
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
	/// Inject dependencies
	/// </summary>
	/// <param name="auth">IAuthDataProvider</param>
	/// <param name="log">ILog</param>
	protected AuthControllerBase(IAuthDataProvider auth, Logging.ILog log) : base(log) =>
		Auth = auth;

	/// <summary>
	/// Display sign in page
	/// </summary>
	/// <param name="returnUrl">[Optional] Return URL</param>
	public IActionResult SignIn(string? returnUrl) =>
		View(SignInModel.Empty(returnUrl ?? Url.Action("Index")));

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
			SignInAsync: HttpContext.SignInAsync,
			ValidateUserAsync: ValidateUserAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedIn =>
				Redirect(GetReturnUrl(Url, model.ReturnUrl)),

			AuthResult.TryAgain =>
				SignIn(model.ReturnUrl),

			_ =>
				Denied(model.ReturnUrl)
		};
	}

	/// <summary>
	/// Perform sign out
	/// </summary>
	public new async Task<IActionResult> SignOut()
	{
		// Get return URL from query
		// (don't add as a method argument or we can't override base method)
		var returnUrl = Request.Query["ReturnUrl"];

		// Do sign out
		var result = await DoSignOutAsync(new(
			AddInfoAlert: TempData.AddInfoAlert,
			SignOutAsync: HttpContext.SignOutAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedOut =>
				RedirectToAction(
					nameof(SignIn),
					new { ReturnUrl = GetReturnUrl(Url, returnUrl) }
				),

			_ =>
				Denied(returnUrl)
		};
	}

	/// <summary>
	/// Show access denied page
	/// </summary>
	/// <param name="returnUrl">Return URL</param>
	public IActionResult Denied(string? returnUrl) =>
		View(new DeniedModel(returnUrl));
}
