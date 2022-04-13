// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Data.Attributes;
using Jeebs.Data.Map;

namespace Jeebs.Auth.Data.Tables;

/// <summary>
/// Authentication Role Table
/// </summary>
public sealed record class AuthRoleTable() : Table(AuthDb.Schema, TableName)
{
	/// <summary>
	/// Table name will be added as a prefix to all column names
	/// </summary>
	public static readonly string TableName = "Role";

	#region From AuthRoleModel

	/// <inheritdoc cref="AuthRoleModel.Id"/>
	[Id]
	public string Id =>
		TableName + nameof(Id);

	/// <inheritdoc cref="AuthRoleModel.Name"/>
	public string Name =>
		TableName + nameof(Name);

	#endregion From AuthRoleModel

	#region From AuthRoleEntity

	/// <inheritdoc cref="AuthRoleEntity.Description"/>
	public string Description =>
		TableName + nameof(Description);

	#endregion From AuthRoleEntity
}
