// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Data;

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
