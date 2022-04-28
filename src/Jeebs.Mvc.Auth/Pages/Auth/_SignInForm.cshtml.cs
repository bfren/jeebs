// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Mvc.Auth.Functions;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Auth.Pages.Auth;

public abstract partial class IndexModel : PageModel
{
	/// <summary>
	/// Get application-specific claims for an authenticated user
	/// </summary>
	protected virtual AuthF.GetClaims? GetClaims { get; }

	/// <summary>
	/// Get standard sign in form
	/// </summary>
	/// <param name="returnUrl">[Optional] Return URL</param>
	public Task<PartialViewResult> OnGetFormAsync(string? returnUrl) =>
		Task.FromResult(Partial("_SignInForm", new Models.SignInModel { ReturnUrl = returnUrl }));

	/// <summary>
	/// Attempt sign in and return result
	/// </summary>
	/// <param name="form">Sign In form data</param>
	public virtual async Task<AuthResult> OnPostFormAsync(Models.SignInModel form)
	{
		Log.Dbg("Performing sign in using {@Form}.", form with { Password = "** REDACTED **" });
		var result = await AuthF.DoSignInAsync(new(
			Model: form,
			Auth: Auth,
			Log: Log,
			Url: Url,
			AddErrorAlert: TempData.AddErrorAlert,
			GetClaims: GetClaims,
			SignInAsync: HttpContext.SignInAsync,
			ValidateUserAsync: AuthF.ValidateUserAsync
		));

		if (result is AuthResult.SignedIn success)
		{
			return success with
			{
				Message = Alert.Success("You were signed in."),
				RedirectTo = AuthF.GetReturnUrl(Url, form.ReturnUrl)
			};
		}

		return result;
	}
}
