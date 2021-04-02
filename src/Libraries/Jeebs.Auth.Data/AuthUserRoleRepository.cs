﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
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
		public Task<Option<AuthUserRoleId>> CreateAsync(AuthUserId userId, AuthRoleId roleId, IDbTransaction? transaction = null)
		{
			var userRole = new AuthUserRoleEntity
			{
				UserId = userId,
				RoleId = roleId
			};

			return CreateAsync(userRole, transaction);
		}
	}
}