// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using StrongId;

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
	public AuthUserId UserId { get; init; } = new();

	/// <summary>
	/// Role ID
	/// </summary>
	public AuthRoleId RoleId { get; init; } = new();

	internal AuthUserRoleEntity() { }
}
