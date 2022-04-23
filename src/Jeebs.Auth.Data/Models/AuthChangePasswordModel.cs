// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data.Models;

/// <summary>
/// Change password model
/// </summary>
/// <param name="Id">User ID</param>
/// <param name="Version">Version</param>
/// <param name="CurrentPassword">User's current password</param>
/// <param name="NewPassword">New password</param>
/// <param name="CheckPassword">Check password</param>
public sealed record class AuthChangePasswordModel(
	AuthUserId Id,
	long Version,
	string CurrentPassword,
	string NewPassword,
	string CheckPassword
) : IWithVersion<AuthUserId>;
