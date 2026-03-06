// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class HttpF
{
	/// <summary>
	/// Retrieve the authorisation header (if it exists)
	/// </summary>
	/// <param name="headers">Dictionary of header values</param>
	public static Result<string> GetAuthorizationHeader(IDictionary<string, StringValues> headers) =>
		headers.TryGetValue("Authorization", out var authorisationHeader) switch
		{
			true when !string.IsNullOrEmpty(authorisationHeader) =>
				authorisationHeader.ToString(),

			_ =>
				R.Fail("Authorization header is missing.")
					.Ctx(nameof(HttpF), nameof(GetAuthorizationHeader))
		};
}
