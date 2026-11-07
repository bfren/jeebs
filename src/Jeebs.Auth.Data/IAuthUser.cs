// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Auth.Data.Ids;

namespace Jeebs.Auth.Data;

/// <summary>
/// Authentication User.
/// </summary>
public interface IAuthUser<TRole> : IWithId<AuthUserId, long>
	where TRole : IAuthRole
{
	/// <summary>
	/// Email address
	/// </summary>
	string EmailAddress { get; init; }

	/// <summary>
	/// Friendly name - option for user interface interaction
	/// </summary>
	string? FriendlyName { get; init; }

	/// <summary>
	/// Given (Christian / first) name
	/// </summary>
	string? GivenName { get; init; }

	/// <summary>
	/// Family name (surname)
	/// </summary>
	string? FamilyName { get; init; }

	/// <summary>
	/// Whether or not the user account has super permissions
	/// </summary>
	bool IsSuper { get; init; }

	/// <summary>
	/// The roles this user is assigned to
	/// </summary>
	List<TRole> Roles { get; init; }
}
