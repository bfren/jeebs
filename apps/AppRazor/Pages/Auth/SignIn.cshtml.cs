// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Logging;

namespace AppRazor.Pages.Auth;

public sealed class SignInModel : Jeebs.Mvc.Razor.Pages.Auth.SignInModel
{
	public SignInModel(IAuthDataProvider auth, ILog<SignInModel> log) : base(auth, log) { }
}
