// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Role model - allows consistent interaction in user interface
	/// </summary>
	public interface IRoleModel
	{
		/// <summary>
		/// Rple ID
		/// </summary>
		public RoleId RoleId { get; init; }

		/// <summary>
		/// Role name
		/// </summary>
		string Name { get; init; }
	}
}
