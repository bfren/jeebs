// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class JwtF
{
	/// <summary>
	/// Extract the token from the authorisation header
	/// </summary>
	/// <param name="authorisationHeader">Authorisation header</param>
	public static Maybe<string> GetToken(string authorisationHeader) =>
		authorisationHeader.StartsWith("Bearer ", StringComparison.InvariantCulture) switch
		{
			true =>
				authorisationHeader["Bearer ".Length..].Trim(),

			_ =>
				F.None<string, M.InvalidAuthorisationHeaderMsg>()
		};

	public static partial class M
	{
		/// <summary>The Authorization header was not a valid Bearer</summary>
		public sealed record class InvalidAuthorisationHeaderMsg : Msg;
	}
}
