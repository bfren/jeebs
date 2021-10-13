// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Data.Models;

/// <summary>
/// Authentication User model
/// </summary>
public record class AuthUserModel : AuthUserWithRoles<AuthRoleModel>
{
	/// <summary>
	/// Create with default values
	/// </summary>
	public AuthUserModel() : this(new(), string.Empty) { }

	/// <summary>
	/// Create with specified values
	/// </summary>
	/// <param name="id">AuthUserId</param>
	/// <param name="email">Email address</param>
	public AuthUserModel(AuthUserId id, string email) =>
		(Id, EmailAddress) = (id, email);
}
