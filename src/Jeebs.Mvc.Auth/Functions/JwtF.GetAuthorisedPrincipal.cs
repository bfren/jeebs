// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using System.Security.Claims;
using Jeebs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Attempt to get an authorised ClaimsPrincipal from the authorisation token.
	/// </summary>
	/// <param name="http">HttpContext</param>
	public static Maybe<ClaimsPrincipal> GetAuthorisedPrincipal(HttpContext http)
	{
		var auth = http.RequestServices.GetRequiredService<IAuthJwtProvider>();
		return from h in GetAuthorisationHeader(http.Request.Headers)
			   from t in GetToken(h)
			   from p in GetPrincipal(auth, t)
			   select p;
	}
}
