// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication Role model
	/// </summary>
	public interface IAuthRoleModel
	{
		/// <summary>
		/// Role ID
		/// </summary>
		public AuthRoleId RoleId { get; init; }

		/// <summary>
		/// Role name
		/// </summary>
		string Name { get; init; }
	}
}
