// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication User Role Table
/// </summary>
public sealed record class AuthUserRoleTable() : Table(AuthDb.Schema, TableName)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names
	/// </summary>
	public static readonly string TableName = "UserRole";

	/// <inheritdoc cref="AuthUserRoleEntity.Id"/>
	[Id]
	public string Id =>
		TableName + nameof(Id);

	/// <inheritdoc cref="AuthUserRoleEntity.UserId"/>
	public string UserId =>
		nameof(UserId);

	/// <inheritdoc cref="AuthUserRoleEntity.RoleId"/>
	public string RoleId =>
		nameof(RoleId);
}
