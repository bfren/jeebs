// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth.Data.Models;

namespace Jeebs.Mvc.Auth.Functions;

/// <summary>
/// Auth functions.
/// </summary>
public static partial class AuthF
{
	/// <summary>
	/// Returns custom claims for a given user.
	/// </summary>
	/// <param name="user">User model</param>
	/// <param name="password">The user's password - required to support encrypted claims</param>
	public delegate Task<List<Claim>> GetClaims(AuthUserModel user, string password);

	/// <summary>Messages</summary>
	public static partial class M { }
}
