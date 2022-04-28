// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Auth.Pages.Auth;

/// <summary>
/// Auth Sign In page model
/// </summary>
public abstract class SignInModel : PageModel
{
	private IAuthDataProvider Auth { get; init; }

	private ILog Log { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="log"></param>
	protected SignInModel(IAuthDataProvider auth, ILog log) =>
		(Auth, Log) = (auth, log);

	/// <summary>
	/// Get page
	/// </summary>
	public virtual IActionResult OnGet() =>
		Page();
}
