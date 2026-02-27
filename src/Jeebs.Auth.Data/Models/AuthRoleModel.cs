// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Ids;

namespace Jeebs.Auth.Data.Models;

/// <summary>
/// Auth Role model.
/// </summary>
public abstract record class AuthRoleModel() : IAuthRole
{
	/// <inheritdoc/>
	public AuthRoleId Id { get; init; } =
		new();

	/// <inheritdoc/>
	public string Name { get; init; } =
		string.Empty;

	/// <summary>
	/// Create with specified values.
	/// </summary>
	/// <param name="id">AuthRoleId.</param>
	/// <param name="name">Role Name.</param>
	public AuthRoleModel(AuthRoleId id, string name) : this() =>
		(Id, Name) = (id, name);
}
