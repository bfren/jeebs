// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	internal static string GetReturnUrl(IUrlHelper url, string? returnUrl) =>
		returnUrl switch
		{
			string x when url.IsLocalUrl(x) =>
				x,

			_ =>
				url.Action("Index") ?? "/"
		};
}
