// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Jwt;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class HttpF
{
	/// <summary>
	/// Extract the token from the authorisation header.
	/// </summary>
	/// <param name="authorisationHeader">Authorisation header.</param>
	/// <returns>JSON Web Token.</returns>
	public static Result<JsonWebToken> GetToken(string authorisationHeader) =>
		authorisationHeader.StartsWith("Bearer ", StringComparison.InvariantCulture) switch
		{
			true =>
				JsonWebToken.Wrap(authorisationHeader["Bearer ".Length..].Trim()),

			_ =>
				R.Fail("Invalid Authorization header.")
					.Ctx(nameof(HttpF), nameof(GetToken))
		};
}
