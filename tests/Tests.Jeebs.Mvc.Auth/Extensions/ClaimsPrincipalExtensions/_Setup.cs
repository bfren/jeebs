// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Claims;

namespace Jeebs.Mvc.Auth.ClaimsPrincipalExtensions_Tests;

public abstract class ClaimsPrincipalExtensions_Tests
{
	protected ClaimsPrincipal Setup(bool authenticated, params Claim[]? claims)
	{
		var claimsIdentity = new ClaimsIdentity(claims, authenticated ? Rnd.Str : null);
		return new([claimsIdentity]);
	}
}
