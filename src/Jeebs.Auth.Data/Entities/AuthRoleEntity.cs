// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Auth.Data.Models;

namespace Jeebs.Auth.Data.Entities
{
	/// <summary>
	/// Authentication Role Entity
	/// </summary>
	public sealed record class AuthRoleEntity : AuthRoleModel, IWithId<AuthRoleId>
	{
		/// <summary>
		/// Role Description
		/// </summary>
		public string Description { get; init; } = string.Empty;

		internal AuthRoleEntity() { }
	}
}
