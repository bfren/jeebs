// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Messages;
using Microsoft.Extensions.Primitives;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Retrieve the authorisation header (if it exists)
	/// </summary>
	/// <param name="headers">Dictionary of header values</param>
	public static Maybe<string> GetAuthorisationHeader(IDictionary<string, StringValues> headers) =>
		headers.TryGetValue("Authorization", out var authorisationHeader) switch
		{
			true when !string.IsNullOrEmpty(authorisationHeader) =>
				authorisationHeader.ToString(),

			_ =>
				F.None<string, M.MissingAuthorisationHeaderMsg>()
		};

	public static partial class M
	{
		/// <summary>Unable to find Authorization header in headers dictionary</summary>
		public sealed record class MissingAuthorisationHeaderMsg : Msg;
	}
}
