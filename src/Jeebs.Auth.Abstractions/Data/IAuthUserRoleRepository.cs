// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication functions for interacting with User Roles
	/// </summary>
	/// <typeparam name="TUserRoleEntity">User Role Entity type</typeparam>
	public interface IAuthUserRoleRepository<TUserRoleEntity> : IRepository<TUserRoleEntity, AuthUserRoleId>
		where TUserRoleEntity : IAuthUserRole, IWithId
	{
		/// <summary>
		/// Create a new User Role
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="roleId">Role ID</param>
		Task<Option<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId);
	}
}