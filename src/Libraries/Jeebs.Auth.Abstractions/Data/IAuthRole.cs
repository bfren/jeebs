// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

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
