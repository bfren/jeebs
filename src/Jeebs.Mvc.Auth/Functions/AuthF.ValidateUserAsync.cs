// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Mvc.Auth.Models;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Validate a user's login credentials and retrieve their info with their roles
	/// </summary>
	/// <param name="auth"></param>
	/// <param name="model"></param>
	internal static Task<Maybe<AuthUserModel>> ValidateUserAsync(IAuthDataProvider auth, SignInModel model) =>
		from _ in auth.ValidateUserAsync<AuthUserModel>(model.Email, model.Password)
		from user in auth.RetrieveUserWithRolesAsync<AuthUserModel, AuthRoleModel>(model.Email)
		select user;
}
