// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Validate a user's login credentials and retrieve their info with their roles.
	/// </summary>
	/// <param name="auth">IAuthDataProvider.</param>
	/// <param name="model">SignInModel.</param>
	/// <param name="log">ILog.</param>
	/// <returns>AuthUserModel.</returns>
	public static Task<Result<AuthUserModel>> ValidateUserAsync(
		IAuthDataProvider auth,
		SignInModel model,
		ILog log
	)
	{
		log.Vrb("Validating credentials for {User}.", model.Email);
		return from v in auth.ValidateUserAsync(model.Email, model.Password)
			   where v
			   from u in auth.RetrieveUserAsync<AuthUserModel, AuthRoleModel>(model.Email)
			   select u;
	}
}
