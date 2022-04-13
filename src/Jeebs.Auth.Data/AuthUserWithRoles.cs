// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data.Attributes;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthUserWithRoles{TRole}"/>
public abstract record class AuthUserWithRoles<TRole> : IAuthUserWithRoles<TRole>
	where TRole : IAuthRole
{
	/// <inheritdoc/>
	public AuthUserId Id { get; init; } = new();

	/// <inheritdoc/>
	public string EmailAddress { get; init; } = string.Empty;

	/// <inheritdoc/>
	public string? FriendlyName { get; init; }

	/// <inheritdoc/>
	public string? GivenName { get; init; }

	/// <inheritdoc/>
	public string? FamilyName { get; init; }

	/// <inheritdoc/>
	public bool IsSuper { get; init; }

	/// <inheritdoc/>
	[Ignore]
	public List<TRole> Roles { get; init; } = new();
}
