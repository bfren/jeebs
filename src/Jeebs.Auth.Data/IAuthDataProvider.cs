// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Data.Models;

namespace Jeebs.Auth.Data;

/// <summary>
/// Auth data provider - provides access to all repositories and db instance.
/// </summary>
public interface IAuthDataProvider
{
	/// <summary>
	/// Database instance.
	/// </summary>
	IAuthDb Db { get; }

	/// <summary>
	/// User Repository.
	/// </summary>
	IAuthUserRepository User { get; }

	/// <summary>
	/// Role Repository.
	/// </summary>
	IAuthRoleRepository Role { get; }

	/// <summary>
	/// User/Role relationship Repository.
	/// </summary>
	IAuthUserRoleRepository UserRole { get; }

	/// <summary>
	/// Change a user's password.
	/// </summary>
	/// <param name="model">AuthChangePasswordModel.</param>
	/// <returns>Whether or not the User's password was changed successfully.</returns>
	Task<Result<bool>> ChangeUserPasswordAsync(AuthChangePasswordModel model);

	/// <summary>
	/// Retrieve the Roles added to the specified User.
	/// </summary>
	/// <typeparam name="TRole">Role type.</typeparam>
	/// <param name="userId">User ID.</param>
	/// <returns>List of roles assigned to the specified User.</returns>
	Task<Result<List<TRole>>> GetRolesForUserAsync<TRole>(AuthUserId userId)
		where TRole : AuthRoleModel;

	/// <summary>
	/// Retrieve a User with their Roles.
	/// </summary>
	/// <typeparam name="TUser">User Model type.</typeparam>
	/// <typeparam name="TRole">Role Model type.</typeparam>
	/// <param name="id">User ID.</param>
	/// <returns>User model.</returns>
	Task<Result<TUser>> RetrieveUserAsync<TUser, TRole>(AuthUserId id)
		where TUser : AuthUserModel<TRole>
		where TRole : AuthRoleModel;

	/// <summary>
	/// Retrieve a User with their Roles.
	/// </summary>
	/// <typeparam name="TUser">User Model type.</typeparam>
	/// <typeparam name="TRole">Role Model type.</typeparam>
	/// <param name="email">User email address.</param>
	/// <returns>User model.</returns>
	Task<Result<TUser>> RetrieveUserAsync<TUser, TRole>(string email)
		where TUser : AuthUserModel<TRole>
		where TRole : AuthRoleModel;

	/// <summary>
	/// Validate a User based on their email and password.
	/// </summary>
	/// <param name="email">Email address.</param>
	/// <param name="password">Password entered by the user.</param>
	/// <returns>Whether or not the User was validated successfully.</returns>
	Task<Result<bool>> ValidateUserAsync(string email, string password);
}
