// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Controllers
{
	/// <inheritdoc cref="AuthController{TProvider}"/>
	public abstract class AuthController : AuthControllerBase
	{
		/// <summary>
		/// AuthDataProvider
		/// </summary>
		new protected AuthDataProvider Auth { get; private init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="auth">AuthDataProvider</param>
		/// <param name="log">ILog</param>
		protected AuthController(AuthDataProvider auth, ILog log) : base(auth, log) =>
			Auth = auth;
	}

	/// <summary>
	/// Implement this controller to add support for user authentication
	/// </summary>
	public abstract class AuthControllerBase : Controller
	{
		/// <summary>
		/// IAuthDataProvider
		/// </summary>
		protected IAuthDataProvider Auth { get; private init; }

		/// <summary>
		/// Add application-specific claims to an authenticated user
		/// </summary>
		protected virtual Func<AuthUserModel, List<Claim>>? AddClaims { get; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="auth">IAuthDataProvider</param>
		/// <param name="log">ILog</param>
		protected AuthControllerBase(IAuthDataProvider auth, ILog log) : base(log) =>
			Auth = auth;

		/// <summary>
		/// Display sign in page
		/// </summary>
		/// <param name="returnUrl">[Optional] Return URL</param>
		public IActionResult SignIn(string? returnUrl) =>
			View(SignInModel.Empty(returnUrl ?? Url.Action("Index")));

		/// <summary>
		/// Perform sign in
		/// </summary>
		/// <param name="model">SignInModel</param>
		[HttpPost, AutoValidateAntiforgeryToken]
		public virtual async Task<IActionResult> SignIn(SignInModel model)
		{
			// Validate user
			var validate = await Auth.ValidateUserAsync<AuthUserModel>(model.Email, model.Password);
			foreach (var user in validate)
			{
				// Get user principal
				Log.Debug("User validated.");
				var principal = GetPrincipal(user);

				// Update last sign in
				var updated = await Auth.UpdateUserLastSignInAsync(user.Id).ConfigureAwait(false);
				updated.AuditSwitch(none: r => Log.Message(r));

				// Add SignIn to HttpContext using Cookie scheme
				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					principal,
					new AuthenticationProperties
					{
						ExpiresUtc = DateTime.UtcNow.AddDays(28),
						IsPersistent = model.RememberMe,
						AllowRefresh = false,
						RedirectUri = model.ReturnUrl
					}
				);

				// Redirect to return url (or Auth/Index)
				return Redirect(GetReturnUrl(model.ReturnUrl));
			}

			// Log error and add alert for user
			Log.Debug("Unknown username or password: {Email}.", model.Email);
			TempData.AddErrorAlert("Unknown username or password.");

			// Return to sign in page
			return SignIn(model.ReturnUrl);
		}

		/// <summary>
		/// Get principal for specified user with all necessary claims
		/// </summary>
		/// <param name="user">User Model</param>
		internal ClaimsPrincipal GetPrincipal(AuthUserModel user)
		{
			// Create claims object
			var claims = new List<Claim>
			{
				new (JwtClaimTypes.UserId, user.Id.Value.ToString(), ClaimValueTypes.Integer32),
				new (ClaimTypes.Name, user.FriendlyName ?? user.EmailAddress, ClaimValueTypes.String),
				new (ClaimTypes.Email, user.EmailAddress, ClaimValueTypes.Email),
			};

			// Add super permission
			if (user.IsSuper)
			{
				claims.Add(new(JwtClaimTypes.IsSuper, true.ToString(), ClaimValueTypes.Boolean));
			}

			// Add roles
			foreach (var role in user.Roles)
			{
				claims.Add(new(ClaimTypes.Role, role.Name, ClaimValueTypes.String));
			}

			// Add custom Claims
			if (AddClaims != null)
			{
				claims.AddRange(AddClaims(user));
			}

			// Create and return identity and principal objects
			var userIdentity = new ClaimsIdentity(claims, "SecureSignIn");
			return new ClaimsPrincipal(userIdentity);
		}

		/// <summary>
		/// Perform sign out
		/// </summary>
		new public async Task<IActionResult> SignOut()
		{
			// Sign out
			await HttpContext.SignOutAsync();

			// Show a friendly message to the user
			TempData.AddInfoAlert("Goodbye!");

			// Redirect to sign in page
			return RedirectToAction(nameof(SignIn), new { ReturnUrl = GetReturnUrl(Request.Query["ReturnUrl"]) });
		}

		/// <summary>
		/// Show access denied page
		/// </summary>
		public IActionResult Denied(string? returnUrl) =>
			View(new DeniedModel(returnUrl));

		/// <summary>
		/// Generate new JWT keys
		/// </summary>
		public IActionResult JwtKeys() =>
			Json(new
			{
				signingKey = F.JwtF.GenerateSigningKey(),
				encryptingKey = F.JwtF.GenerateEncryptingKey()
			});

		/// <summary>
		/// Return either <paramref name="returnUrl"/> or Index action
		/// </summary>
		/// <param name="returnUrl">Return URL</param>
		private string GetReturnUrl(string? returnUrl) =>
			returnUrl switch
			{
				string url when Url.IsLocalUrl(url) =>
					url,

				_ =>
					Url.Action("Index")
			};
	}
}
