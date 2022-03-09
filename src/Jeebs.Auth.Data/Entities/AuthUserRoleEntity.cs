// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Id;

namespace Jeebs.Auth.Data.Entities;

/// <summary>
/// Authentication User Role
/// </summary>
public sealed record class AuthUserRoleEntity : IWithId<AuthUserRoleId>, IAuthUserRole
{
	/// <summary>
	/// User Role ID
	/// </summary>
	[Id]
	public AuthUserRoleId Id { get; init; } = new();

	/// <summary>
	/// User ID
	/// </summary>
	public AuthUserId UserId { get; init; }

	/// <summary>
	/// Role ID
	/// </summary>
	public AuthRoleId RoleId { get; init; }

	internal AuthUserRoleEntity() { }
}
