// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Ids;

namespace Jeebs.Auth.Data;

/// <summary>
/// Authentication User Role relationship.
/// </summary>
public interface IAuthUserRole : IWithId<AuthUserRoleId, long>
{
	/// <summary>
	/// User ID.
	/// </summary>
	AuthUserId UserId { get; init; }

	/// <summary>
	/// Role ID.
	/// </summary>
	AuthRoleId RoleId { get; init; }
}
