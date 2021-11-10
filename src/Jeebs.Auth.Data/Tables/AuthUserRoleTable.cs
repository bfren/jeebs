// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Mapping;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Role Table
/// </summary>
public sealed record class AuthUserRoleTable() : Table("Auth", ColumnPrefix)
{
	/// <summary>
	/// Prefix added before all columns
	/// </summary>
	public const string ColumnPrefix = "UserRole";

	/// <inheritdoc cref="AuthUserRoleEntity.Id"/>
	public string Id =>
		ColumnPrefix + nameof(Id);

	/// <inheritdoc cref="AuthUserRoleEntity.UserId"/>
	public string UserId =>
		nameof(UserId);

	/// <inheritdoc cref="AuthUserRoleEntity.RoleId"/>
	public string RoleId =>
		nameof(RoleId);
}
