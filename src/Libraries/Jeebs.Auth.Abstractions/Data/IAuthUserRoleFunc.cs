// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication functions for interacting with User Roles
	/// </summary>
	/// <typeparam name="TUserRoleEntity">User Role Entity type</typeparam>
	public interface IAuthUserRoleFunc<TUserRoleEntity> : IDbFunc<TUserRoleEntity, AuthUserRoleId>
		where TUserRoleEntity : IAuthUserRole, IEntity
	{
		/// <summary>
		/// Create a new User Role
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="roleId">Role ID</param>
		Task<Option<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId);
	}
}