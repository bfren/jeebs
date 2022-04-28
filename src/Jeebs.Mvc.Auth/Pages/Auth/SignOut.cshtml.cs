// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Functions;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Auth.Pages.Auth;

/// <summary>
/// Auth Sign Out page model
/// </summary>
public abstract class SignOutModel : PageModel
{
	private IAuthDataProvider Auth { get; init; }

	private ILog Log { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="log"></param>
	protected SignOutModel(IAuthDataProvider auth, ILog log) =>
		(Auth, Log) = (auth, log);

	/// <summary>
	/// Attempt sign out and return result
	/// </summary>
	/// <param name="returnUrl">[Optional] Return URL</param>
	public virtual async Task<IActionResult> OnGetAsync(string? returnUrl)
	{
		// Do sign out
		var result = await AuthF.DoSignOutAsync(new(
			AddInfoAlert: TempData.AddInfoAlert,
			SignOutAsync: HttpContext.SignOutAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedOut =>
				Result.Create(true, Alert.Success("You were signed out.")) with
				{
					RedirectTo = AuthF.GetReturnUrl(Url, returnUrl)
				},

			_ =>
				Forbid()
		};
	}
}
