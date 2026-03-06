// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthUserRoleRepository"/>
public sealed class AuthUserRoleRepository : Repository<AuthUserRoleEntity, AuthUserRoleId>, IAuthUserRoleRepository
{
	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb.</param>
	/// <param name="log">ILog.</param>
	public AuthUserRoleRepository(IAuthDb db, ILog<AuthUserRoleRepository> log) : base(db, log) { }

	/// <inheritdoc/>
	public Task<Result<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId)
	{
		var userRole = new AuthUserRoleEntity
		{
			UserId = userId,
			RoleId = roleId
		};

		return CreateAsync(userRole);
	}
}
