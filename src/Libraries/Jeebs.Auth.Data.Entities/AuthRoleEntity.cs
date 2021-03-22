// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities
{
	/// <inheritdoc cref="IAuthRole"/>
	public abstract record AuthRoleEntity : IAuthRole
	{
		/// <inheritdoc/>
		[Ignore]
		StrongId IEntity.Id
		{
			get =>
				RoleId;

			init =>
				RoleId = new AuthRoleId { Value = value.Value };
		}

		/// <inheritdoc/>
		[Id]
		public AuthRoleId RoleId { get; init; } = new AuthRoleId();

		/// <inheritdoc/>
		public string Name { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string Description { get; init; } = string.Empty;
	}
}
