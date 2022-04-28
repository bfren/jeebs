// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jeebs.Mvc.Auth.Pages.Auth;

public abstract partial class IndexModel : PageModel
{
	/// <summary>
	/// Show access denied page
	/// </summary>
	/// <param name="returnUrl">Return URL</param>
	public virtual Task<PartialViewResult> OnGetDeniedAsync(string? returnUrl) =>
		Task.FromResult(Partial("_Denied", new DeniedModel(returnUrl)));
}
