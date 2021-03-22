// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Controllers
{
	/// <summary>
	/// Implement this controller to add support for user authentication
	/// </summary>
	/// <typeparam name="TUser">User type</typeparam>
	public abstract class AuthController<TUser> : Controller
		where TUser : IUserModel, IAuthUser
	{
		/// <summary>
		/// IDataAuthProvider
		/// </summary>
		protected IAuthDataProvider<TUser> Auth { get; init; }

		/// <summary>
		/// Add application-specific claims to an authenticated user
		/// </summary>
		protected virtual Func<TUser, List<Claim>>? AddClaims { get; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="auth">IDataAuthProvider</param>
		/// <param name="log">ILog</param>
		protected AuthController(IAuthDataProvider<TUser> auth, ILog log) : base(log) =>
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
			var validate = await ValidateUserAsync(model.Email, model.Password);
			if (validate is Some<TUser> user)
			{
				// Get user principal
				Log.Debug("User validated.");
				var principal = GetPrincipal(user.Value);

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
		/// Validate user using <see cref="Auth"/>
		/// </summary>
		/// <param name="email">User email</param>
		/// <param name="password">User password</param>
		internal Task<Option<TUser>> ValidateUserAsync(string email, string password) =>
			Auth.ValidateUserAsync(email, password);

		/// <summary>
		/// Get principal for specified user with all necessary claims
		/// </summary>
		/// <param name="user">User entity</param>
		internal ClaimsPrincipal GetPrincipal(TUser user)
		{
			// Create claims object
			var claims = new List<Claim>
			{
				new (JwtClaimTypes.UserId, user.UserId.Value.ToString(), ClaimValueTypes.Integer32),
				new (ClaimTypes.Name, user.FriendlyName, ClaimValueTypes.String),
				new (ClaimTypes.Email, user.EmailAddress, ClaimValueTypes.Email),
			};

			if (user.IsSuper)
			{
				claims.Add(new(JwtClaimTypes.IsSuper, true.ToString(), ClaimValueTypes.Boolean));
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
