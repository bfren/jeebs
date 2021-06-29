// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthUserRoleRepository{TUserRoleEntity}"/>
	public interface IAuthUserRoleRepository : IAuthUserRoleRepository<AuthUserRoleEntity>
	{ }

	/// <inheritdoc cref="IAuthUserRoleRepository{TUserRoleEntity}"/>
	public sealed class AuthUserRoleRepository : Repository<AuthUserRoleEntity, AuthUserRoleId>, IAuthUserRoleRepository
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		public AuthUserRoleRepository(IAuthDb db, ILog<AuthUserRoleRepository> log) : base(db, log) { }

		/// <inheritdoc/>
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
