// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Attempt to get the authorised ClaimsPrincipal
	/// </summary>
	/// <param name="ctx">AuthorizationFilterContext</param>
	public static Maybe<ClaimsPrincipal> GetAuthorisedPrincipal(AuthorizationFilterContext ctx)
	{
		var auth = ctx.HttpContext.RequestServices.GetRequiredService<IAuthJwtProvider>();
		return from h in GetAuthorisationHeader(ctx.HttpContext.Request.Headers)
			   from t in GetToken(h)
			   from p in GetPrincipal(auth, t)
			   select p;
	}
}
