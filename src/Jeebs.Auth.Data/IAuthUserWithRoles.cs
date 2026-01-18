// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;

namespace Jeebs.Auth.Data;

/// <summary>
/// Authentication User with list of Roles.
/// </summary>
/// <typeparam name="TRole">Role model type</typeparam>
public interface IAuthUserWithRoles<TRole> : IAuthUser
	where TRole : IAuthRole
{
	/// <summary>
	/// The roles this user is assigned to
	/// </summary>
	List<TRole> Roles { get; init; }
}
