// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data.Models
{
	/// <summary>
	/// Authentication Role model
	/// </summary>
	public record AuthRoleModel : IAuthRoleModel
	{
		/// <summary>
		/// Role ID
		/// </summary>
		[Id]
		public AuthRoleId Id { get; init; }

		/// <summary>
		/// Role Name (should be a normalised string)
		/// </summary>
		public string Name { get; init; }

		/// <summary>
		/// Create with default values
		/// </summary>
		public AuthRoleModel() : this(new(), string.Empty) { }

		/// <summary>
		/// Create with specified values
		/// </summary>
		/// <param name="id">AuthRoleId</param>
		/// <param name="name">Role Name</param>
		public AuthRoleModel(AuthRoleId id, string name) =>
			(Id, Name) = (id, name);
	}
}
