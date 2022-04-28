// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Razor.Pages.Auth;

/// <summary>
/// Auth Index page
/// </summary>
[Authorize]
public abstract partial class IndexModel : PageModel
{
	/// <summary>
	/// Log
	/// </summary>
	protected ILog Log { get; init; }

	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="log"></param>
	protected IndexModel(ILog log) =>
		Log = log;

	/// <summary>
	/// Redirect to Sign In page
	/// </summary>
	public virtual IActionResult OnGet() =>
		RedirectToPage("/Auth/SignIn");
}
