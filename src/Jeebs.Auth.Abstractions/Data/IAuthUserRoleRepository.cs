// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
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
		/// <param name="transaction">[Optional] Transaction</param>
		Task<Option<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId, IDbTransaction? transaction = null);
	}
}