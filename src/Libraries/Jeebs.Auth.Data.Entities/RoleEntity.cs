// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Mapping;

namespace Jeebs.Auth.Data.Entities
{
	/// <inheritdoc cref="IAuthRole"/>
	internal sealed record RoleEntity : IAuthRole
	{
		/// <inheritdoc/>
		[Ignore]
		StrongId IEntity.Id
		{
			get =>
				RoleId;

			init =>
				RoleId = new RoleId(value.Value);
		}

		/// <inheritdoc/>
		[Id]
		public RoleId RoleId { get; init; } = new RoleId();

		/// <inheritdoc/>
		public string Name { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string Description { get; init; } = string.Empty;
	}
}
