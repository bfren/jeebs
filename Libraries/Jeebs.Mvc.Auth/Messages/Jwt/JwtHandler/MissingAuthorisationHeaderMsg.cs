// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs;
using Microsoft.Extensions.Primitives;

namespace Jm.Mvc.Auth.Jwt.JwtHandler
{
	/// <summary>
	/// See <see cref="Jeebs.Mvc.Auth.Jwt.JwtHandler.GetAuthorisationHeader(IDictionary{string, StringValues})"/>
	/// </summary>
	public sealed class MissingAuthorisationHeaderMsg : IMsg { }
}
