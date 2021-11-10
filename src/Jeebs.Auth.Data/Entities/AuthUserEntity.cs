// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs.Auth.Data.Models;
using Jeebs.Data.Entities;

namespace Jeebs.Auth.Data.Entities;

/// <summary>
/// Authentication User Entity
/// </summary>
public sealed record class AuthUserEntity : AuthUserModel, IWithVersion
{
	/// <inheritdoc/>
	[Version]
	public long Version { get; init; }

	/// <summary>
	/// The user's encrypted password
	/// </summary>
	public string PasswordHash { get; init; } = string.Empty;

	/// <summary>
	/// TOTP secret - used for generating one-time authentication codes
	/// </summary>
	public string? TotpSecret { get; init; }

	/// <summary>
	/// TOTP backup codes - used when the authenticator is not available
	/// </summary>
	public List<string> TotpBackupCodes { get; init; } = new();

	/// <summary>
	/// Whether or not the user account is enabled
	/// </summary>
	public bool IsEnabled { get; init; }

	/// <summary>
	/// The last time the user signed in
	/// </summary>
	public DateTimeOffset? LastSignedIn { get; init; }

	internal AuthUserEntity() { }

	internal AuthUserEntity(AuthUserId id, string email, string passwordHash) : base(id, email) =>
		PasswordHash = passwordHash;
}
