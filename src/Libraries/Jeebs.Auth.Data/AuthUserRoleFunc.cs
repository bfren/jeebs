// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthUserRoleFunc{TUserRoleEntity}"/>
	public interface IAuthUserRoleFunc : IAuthUserRoleFunc<AuthUserRoleEntity>
	{ }

	/// <inheritdoc cref="IAuthUserRoleFunc{TUserRoleEntity}"/>
	public sealed class AuthUserRoleFunc : DbFunc<AuthUserRoleEntity, AuthUserRoleId>, IAuthUserRoleFunc
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		public AuthUserRoleFunc(IAuthDb db, ILog<AuthUserRoleFunc> log) : base(db, log) { }


		/// <summary>
		/// Create a new User Role
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="roleId">Role ID</param>
		public Task<Option<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId)
		{
			var userRole = new AuthUserRoleEntity
			{
				UserId = userId,
				RoleId = roleId
			};

			return CreateAsync(userRole);
		}
	}
}
