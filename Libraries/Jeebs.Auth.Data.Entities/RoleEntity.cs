// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data.Entities
{
	/// <inheritdoc cref="IRole"/>
	internal sealed record RoleEntity : IRole
	{
		/// <inheritdoc/>
		public IStrongId<long> Id
		{
			get =>
				RoleId;

			init =>
				RoleId = new RoleId(value.Value);
		}

		/// <inheritdoc/>
		public RoleId RoleId { get; init; } = new RoleId();

		/// <inheritdoc/>
		public string Name { get; init; } = string.Empty;

		/// <inheritdoc/>
		public string Description { get; init; } = string.Empty;
	}
}
