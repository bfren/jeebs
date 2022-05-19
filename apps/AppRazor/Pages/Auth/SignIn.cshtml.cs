// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Config.Web.Auth;
using Jeebs.Logging;
using Microsoft.Extensions.Options;

namespace AppRazor.Pages.Auth;

public sealed class SignInModel : Jeebs.Mvc.Razor.Pages.Auth.SignInModel
{
	public SignInModel(IAuthDataProvider auth, IAuthJwtProvider jwt, IOptions<AuthConfig> config, ILog<SignInModel> log) : base(auth, jwt, config, log) { }
}
