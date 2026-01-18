// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Config.Web.Auth;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Jeebs.Mvc.Razor.Pages.Auth;

/// <summary>
/// Auth Sign In page model.
/// </summary>
public abstract partial class SignInModel : PageModel
{
	/// <summary>
	/// Auth Data provider.
	/// </summary>
	protected IAuthDataProvider Auth { get; init; }

	/// <summary>
	/// Auth JWT provider.
	/// </summary>
	protected IAuthJwtProvider Jwt { get; init; }

	/// <summary>
	/// Log.
	/// </summary>
	protected ILog Log { get; init; }

	/// <summary>
	/// Auth Config.
	/// </summary>
	protected AuthConfig Config { get; init; }

	/// <summary>
	/// Get application-specific claims for an authenticated user.
	/// </summary>
	protected virtual AuthF.GetClaims? GetClaims { get; }

	/// <summary>
	/// Redirect here after a successful sign in.
	/// </summary>
	protected virtual Func<string?> SignInRedirect { get; init; }

	/// <summary>
	/// Model to use for Sign In form partial.
	/// </summary>
	public Mvc.Auth.Models.SignInModel PartialModel { get; set; } = new();

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="jwt"></param>
	/// <param name="config"></param>
	/// <param name="log"></param>
	protected SignInModel(IAuthDataProvider auth, IAuthJwtProvider jwt, IOptions<AuthConfig> config, ILog log) =>
		(Auth, Jwt, Log, Config, SignInRedirect) = (auth, jwt, log, config.Value, () => Url.Page("/Index"));

}
