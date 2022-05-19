// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Config.Web.Auth;
using Jeebs.Mvc.Auth;
using Jeebs.Mvc.Auth.Functions;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Razor.Pages.Auth;

public abstract partial class SignInModel
{
	/// <summary>
	/// Get standard sign in form
	/// </summary>
	public Task<PartialViewResult> OnGetFormAsync() =>
		Task.FromResult(Partial("_SignInForm", new Mvc.Auth.Models.SignInModel()));

	/// <summary>
	/// Attempt sign in and return result
	/// </summary>
	/// <param name="form">Sign In form data</param>
	public virtual async Task<AuthResult> OnPostFormAsync(Mvc.Auth.Models.SignInModel form)
	{
		Log.Dbg("Performing sign in using {@Form}.", form with { Password = "** REDACTED **" });
		var result = await AuthF.DoSignInAsync(new(form, Auth, Log, Url, GetClaims,
			SignInAsync: Config.Scheme switch
			{
				AuthScheme.Cookies =>
					user => AuthF.CreateCookieAsync(HttpContext, user, form.RememberMe, SignInRedirect()),

				AuthScheme.Jwt =>
					user => AuthF.CreateTokenAsync(Jwt, user, Log),

				_ =>
					async _ => await Task.FromResult(new AuthResult.Denied())
			}
		));

		return result switch
		{
			AuthResult.SignedIn =>
				result with { Message = Alert.Success("You were signed in.") },

			_ =>
				result
		};
	}
}
