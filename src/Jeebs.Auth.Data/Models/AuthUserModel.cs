// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Attributes;

namespace Jeebs.Auth.Data.Models;

/// <summary>
/// Auth User model.
/// </summary>
public abstract record class AuthUserModel<TRole>() : IAuthUser<TRole>
	where TRole : AuthRoleModel
{
	/// <inheritdoc/>
	public AuthUserId Id { get; init; } =
		new();

	/// <inheritdoc/>
	public string EmailAddress { get; init; } =
		string.Empty;

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
	public List<TRole> Roles { get; init; } =
		[];

	/// <summary>
	/// Create with specified values.
	/// </summary>
	/// <param name="id">AuthUserId.</param>
	/// <param name="email">Email address.</param>
	public AuthUserModel(AuthUserId id, string email) : this() =>
		(Id, EmailAddress) = (id, email);
}
