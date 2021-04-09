// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication Role Model
	/// </summary>
	public interface IAuthRole : IWithId<AuthRoleId>
	{
		/// <summary>
		/// Role Name (should be a normalised string)
		/// </summary>
		string Name { get; init; }
	}
}
