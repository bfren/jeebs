// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Auth Role Table.
/// </summary>
public sealed record class AuthRoleTable() : Table(AuthDb.Schema, TableName)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names.
	/// </summary>
	public static readonly string TableName = "role";

	#region From AuthRoleModel

	/// <inheritdoc cref="AuthRoleModel.Id"/>
	[Id]
	public string Id =>
		"role_id";

	/// <inheritdoc cref="AuthRoleModel.Name"/>
	public string Name =>
		"role_name";

	#endregion From AuthRoleModel

	#region From AuthRoleEntity

	/// <inheritdoc cref="AuthRoleEntity.Version"/>
	[Version]
	public string Version =>
		"role_version";

	/// <inheritdoc cref="AuthRoleEntity.Description"/>
	public string Description =>
		"role_description";

	#endregion From AuthRoleEntity
}
