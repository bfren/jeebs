// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Data.Models;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Entities;

/// <summary>
/// Auth User entity.
/// </summary>
public sealed record class AuthUserEntity : AuthUserModel<AuthRoleEntity>, IWithVersion<AuthUserId, long>
{
	/// <inheritdoc/>
	public long Version { get; init; }

	/// <summary>
	/// The user's encrypted password.
	/// </summary>
	public string PasswordHash { get; init; } =
		string.Empty;

	/// <summary>
	/// Whether or not the user account is enabled.
	/// </summary>
	public bool IsEnabled { get; init; }

	/// <summary>
	/// The last time the user signed in.
	/// </summary>
	public DateTimeOffset? LastSignedIn { get; init; }

	/// <summary>
	/// Create blank object.
	/// </summary>
	public AuthUserEntity() { }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="id">AuthUserId.</param>
	/// <param name="email">Email Address.</param>
	/// <param name="passwordHash">Hashed password.</param>
	public AuthUserEntity(AuthUserId id, string email, string passwordHash) :
		base(id, email) =>
		PasswordHash = passwordHash;
}
