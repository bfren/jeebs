// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppRazor.Pages.Auth;

[Authorize("Token")]
public class CheckModel : PageModel
{
	public void OnGet()
	{
	}
}
