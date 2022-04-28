// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace AppRazor.Pages.Auth;

public sealed class SignInModel : Jeebs.Mvc.Razor.Pages.Auth.SignInModel
{
	public SignInModel(ILog<SignInModel> log) : base(log) { }
}
