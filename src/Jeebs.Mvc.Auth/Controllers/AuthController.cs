// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Jwt.Functions;
using Jeebs.Mvc.Auth.Functions;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

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
	/// Add application-specific claims to an authenticated user
	/// </summary>
	protected virtual AuthF.GetClaims? AddClaims { get; }

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
	[HttpPost, AutoValidateAntiforgeryToken]
	public virtual Task<IActionResult> SignIn(SignInModel model) =>
		AuthF.DoSignInAsync(new(
			Model: model,
			AddClaims: AddClaims,
			AddErrorAlert: TempData.AddErrorAlert,
			Auth: Auth,
			GetRedirect: Redirect,
			Log: Log,
			SignInAsync: HttpContext.SignInAsync,
			GetSignInFormPage: SignIn,
			Url: Url
		));

	/// <summary>
	/// Perform sign out
	/// </summary>
	public new Task<IActionResult> SignOut() =>
		AuthF.DoSignOutAsync(new(
			AddInfoAlert: TempData.AddInfoAlert,
			GetSignInFormPage: () =>
				RedirectToAction(
					nameof(SignIn),
					new { ReturnUrl = AuthF.GetReturnUrl(Url, Request.Query["ReturnUrl"]) }
				),
			SignOutAsync: HttpContext.SignOutAsync
		));

	/// <summary>
	/// Show access denied page
	/// </summary>
	/// <param name="returnUrl">Return URL</param>
	public IActionResult Denied(string? returnUrl) =>
		View(new DeniedModel(returnUrl));

	/// <summary>
	/// Generate new JWT keys
	/// </summary>
	public IActionResult JwtKeys() =>
		Json(new
		{
			signingKey = JwtF.GenerateSigningKey(),
			encryptingKey = JwtF.GenerateEncryptingKey()
		});
}
