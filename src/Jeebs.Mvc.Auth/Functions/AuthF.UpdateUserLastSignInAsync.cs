// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Logging;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Update the last sign in for a user
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="user"></param>
	/// <param name="log"></param>
	public static Task<Maybe<bool>> UpdateUserLastSignInAsync(IAuthDataProvider auth, AuthUserId user, ILog log)
	{
		log.Vrb("Updating last sign in for user {UserId}", user.Value);
		return auth.User.UpdateLastSignInAsync(user)
			.AuditAsync(none: log.Msg);
	}
}
