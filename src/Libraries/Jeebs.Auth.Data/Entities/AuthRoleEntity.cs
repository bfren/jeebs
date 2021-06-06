// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Auth.Data.Models;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities
{
	/// <summary>
	/// Authentication Role Entity
	/// </summary>
	public sealed record AuthRoleEntity : AuthRoleModel, IEntity<AuthRoleId>
	{
		/// <summary>
		/// Role Description
		/// </summary>
		public string Description { get; init; } = string.Empty;

		internal AuthRoleEntity() { }
	}
}
