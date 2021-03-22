// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// Authentication User Role Table
	/// </summary>
	public sealed record AuthUserRoleTable() : Table("auth_user_role")
	{
		/// <inheritdoc cref="AuthUserRoleEntity.UserRoleId"/>
		public string UserRoleId =>
			nameof(UserRoleId);

		/// <inheritdoc cref="AuthUserRoleEntity.UserId"/>
		public string UserId =>
			nameof(UserId);

		/// <inheritdoc cref="AuthUserRoleEntity.RoleId"/>
		public string RoleId =>
			nameof(RoleId);
	}
}
