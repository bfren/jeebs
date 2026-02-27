// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities;

/// <summary>
/// Auth Role entity.
/// </summary>
public sealed record class AuthRoleEntity : AuthRoleModel, IWithVersion<AuthRoleId, long>
{
	/// <inheritdoc/>
	public long Version { get; init; }

	/// <summary>
	/// Role Description.
	/// </summary>
	public string Description { get; init; } =
		string.Empty;
}
