// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// User Role Table
	/// </summary>
	public sealed record UserRoleTable : Table
	{
		/// <inheritdoc cref="UserRoleEntity.UserRoleId"/>
		public string UserRoleId =>
			nameof(UserRoleId);

		/// <inheritdoc cref="UserRoleEntity.UserId"/>
		public string UserId =>
			nameof(UserId);

		/// <inheritdoc cref="UserRoleEntity.RoleId"/>
		public string RoleId =>
			nameof(RoleId);

		/// <summary>
		/// Create object
		/// </summary>
		public UserRoleTable() : base("auth_user_role") { }
	}
}
