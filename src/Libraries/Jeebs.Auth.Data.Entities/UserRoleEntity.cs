// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities
{
	/// <inheritdoc cref="IAuthRole"/>
	internal sealed record UserRoleEntity : IEntity
	{
		/// <inheritdoc/>
		[Ignore]
		StrongId IEntity.Id
		{
			get =>
				UserRoleId;

			init =>
				UserRoleId = new UserRoleId(value.Value);
		}

		/// <summary>
		/// User Role ID
		/// </summary>
		[Id]
		public UserRoleId UserRoleId { get; init; } = new();

		/// <summary>
		/// User ID
		/// </summary>
		public UserId UserId { get; init; } = new();

		/// <summary>
		/// Role ID
		/// </summary>
		public RoleId RoleId { get; init; } = new();
	}
}
