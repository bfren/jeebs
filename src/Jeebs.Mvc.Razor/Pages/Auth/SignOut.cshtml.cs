// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs.Logging;
using Jeebs.Mvc.Auth;
using Jeebs.Mvc.Auth.Functions;
using Jeebs.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Razor.Pages.Auth;

/// <summary>
/// Auth Sign Out page model
/// </summary>
[Authorize]
public abstract class SignOutModel : PageModel
{
	/// <summary>
	/// Log
	/// </summary>
	protected ILog Log { get; init; }

	/// <summary>
	/// Redirect here after a successful sign out
	/// </summary>
	protected virtual Func<string?> SignOutRedirect { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="log"></param>
	protected SignOutModel(ILog log) =>
		(Log, SignOutRedirect) = (log, () => Url.Page("/Auth/SignIn"));

	/// <summary>
	/// Attempt sign out and return result
	/// </summary>
	/// <param name="returnUrl">[Optional] Return URL</param>
	public virtual async Task<IActionResult> OnGetAsync(string? returnUrl)
	{
		// Do sign out
		var result = await AuthF.DoSignOutAsync(new(
			AddInfoAlert: TempData.AddInfoAlert,
			RedirectUrl: SignOutRedirect,
			SignOutAsync: HttpContext.SignOutAsync
		));

		// Handle result
		return result switch
		{
			AuthResult.SignedOut =>
				result with { Message = Alert.Success("You were signed out.") },

			_ =>
				Forbid()
		};
	}
}
