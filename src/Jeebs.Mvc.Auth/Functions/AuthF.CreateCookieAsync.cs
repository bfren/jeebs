// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Create authentication cookie.
	/// </summary>
	/// <param name="http">HttpContext</param>
	/// <param name="user">ClaimsPrincipal</param>
	/// <param name="persist">If true, cookie will be marked as persistent</param>
	/// <param name="redirectUrl">[Optional] URL to redirect to on success</param>
	public static async Task<AuthResult> CreateCookieAsync(HttpContext http, ClaimsPrincipal user, bool persist, string? redirectUrl)
	{
		await http.SignInAsync(user, new()
		{
			IssuedUtc = DateTime.UtcNow,
			ExpiresUtc = DateTime.UtcNow.AddDays(28),
			IsPersistent = persist
		});

		return new AuthResult.SignedIn(redirectUrl);
	}
}
