// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// Authentication Role Table
	/// </summary>
	public sealed record AuthRoleTable() : Table("auth_role")
	{
		/// <summary>
		/// Prefix added before all columns
		/// </summary>
		public const string Prefix = "Role";

		/// <inheritdoc cref="AuthRoleEntity.RoleId"/>
		public string RoleId =>
			nameof(RoleId);

		/// <inheritdoc cref="AuthRoleEntity.Name"/>
		public string Name =>
			Prefix + nameof(Name);

		/// <inheritdoc cref="AuthRoleEntity.Description"/>
		public string Description =>
			Prefix + nameof(Description);
	}
}
