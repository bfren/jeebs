// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities
{
	/// <summary>
	/// Authentication User Role
	/// </summary>
	public sealed record AuthUserRoleEntity : IEntity<AuthUserRoleId>
	{
		/// <summary>
		/// User Role ID
		/// </summary>
		[Id]
		public AuthUserRoleId Id { get; init; } = new();

		/// <summary>
		/// User ID
		/// </summary>
		public AuthUserId UserId { get; init; } = new();

		/// <summary>
		/// Role ID
		/// </summary>
		public AuthRoleId RoleId { get; init; } = new();
	}
}
