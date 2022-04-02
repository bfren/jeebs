// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Auth.Data.Models;

/// <summary>
/// Authentication Role model
/// </summary>
public record class AuthRoleModel : IAuthRole
{
	/// <summary>
	/// Role ID
	/// </summary>
	[Id]
	public AuthRoleId Id { get; init; } = new();

	/// <summary>
	/// Role Name (should be a normalised string)
	/// </summary>
	public string Name { get; init; }

	/// <summary>
	/// Create with default values
	/// </summary>
	public AuthRoleModel() : this(new(), string.Empty) { }

	/// <summary>
	/// Create with specified values
	/// </summary>
	/// <param name="id">AuthRoleId</param>
	/// <param name="name">Role Name</param>
	public AuthRoleModel(AuthRoleId id, string name) =>
		(Id, Name) = (id, name);
}
