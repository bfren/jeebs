// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Repository;

namespace Jeebs.Auth.Data;

/// <summary>
/// Auth User/Role relationship Repository.
/// </summary>
public interface IAuthUserRoleRepository : IRepository<AuthUserRoleEntity, AuthUserRoleId>
{
	/// <summary>
	/// Create a new User Role.
	/// </summary>
	/// <param name="userId">User ID.</param>
	/// <param name="roleId">Role ID.</param>
	/// <returns>New User/Role relationship ID.</returns>
	Task<Result<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId);
}
