// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Ids;

namespace Jeebs.Auth.Data;

/// <summary>
/// Authentication Role.
/// </summary>
public interface IAuthRole : IWithId<AuthRoleId, long>
{
	/// <summary>
	/// Role Name (should be a normalised string).
	/// </summary>
	string Name { get; init; }
}
