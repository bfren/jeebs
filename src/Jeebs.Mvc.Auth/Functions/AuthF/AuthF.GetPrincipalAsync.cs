// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth.Jwt.Constants;
using Jeebs.Mvc.Auth.Models;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Get a User Principal with claims added.
	/// </summary>
	/// <param name="user">AuthUserModel.</param>
	/// <param name="password">Password (used to get encrypted claims).</param>
	/// <param name="getClaims">GetClaims delegate.</param>
	internal static async Task<ClaimsPrincipal> GetPrincipalAsync(
		AuthUserModel user,
		string password,
		GetClaims? getClaims
	)
	{
		// Create claims object
		var claims = new List<Claim>
		{
			new (JwtClaimTypes.UserId, user.Id.Value.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer32),
			new (ClaimTypes.Name, user.FriendlyName ?? user.EmailAddress, ClaimValueTypes.String),
			new (ClaimTypes.Email, user.EmailAddress, ClaimValueTypes.Email),
		};

		// Add super permission
		if (user.IsSuper)
		{
			claims.Add(new(JwtClaimTypes.IsSuper, true.ToString(), ClaimValueTypes.Boolean));
		}

		// Add roles
		foreach (var role in user.Roles)
		{
			claims.Add(new(ClaimTypes.Role, role.Name, ClaimValueTypes.String));
		}

		// Add custom Claims
		if (getClaims != null)
		{
			claims.AddRange(await getClaims(user, password));
		}

		// Create and return identity and principal objects
		var userIdentity = new ClaimsIdentity(claims, "SecureSignIn");
		return new ClaimsPrincipal(userIdentity);
	}
}
