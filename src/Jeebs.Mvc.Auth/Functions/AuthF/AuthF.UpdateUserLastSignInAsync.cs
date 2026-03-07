// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Ids;
using Jeebs.Logging;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Update the last sign in for a user.
	/// </summary>
	/// <param name="auth">IAuthDataProvider.</param>
	/// <param name="user">AuthUserId.</param>
	/// <param name="log">ILog.</param>
	/// <returns>Success or failure.</returns>
	public static async Task<bool> UpdateUserLastSignInAsync(
		IAuthDataProvider auth,
		AuthUserId user,
		ILog log
	)
	{
		log.Vrb("Updating last sign in for user {UserId}.", user.Value);
		return await auth.User
			.UpdateLastSignInAsync(user)
			.IfFailedAsync(log.Failure)
			.UnwrapAsync(ifFailed: _ => false);
	}
}
