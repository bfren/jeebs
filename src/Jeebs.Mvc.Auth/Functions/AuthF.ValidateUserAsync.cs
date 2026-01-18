// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Validate a user's login credentials and retrieve their info with their roles.
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="model"></param>
	/// <param name="log"></param>
	public static Task<Maybe<AuthUserModel>> ValidateUserAsync(IAuthDataProvider auth, SignInModel model, ILog log)
	{
		log.Vrb("Validating credentials for {User}.", model.Email);
		return from _ in auth.ValidateUserAsync<AuthUserModel>(model.Email, model.Password)
			   from user in auth.RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(model.Email)
			   select user;
	}
}
