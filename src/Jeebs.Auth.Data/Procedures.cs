// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Auth.Data;

/// <summary>
/// Stored Procedure names.
/// </summary>
public static class Procedures
{
	/// <summary>
	/// Update user's last sign in.
	/// </summary>
	public static string UpdateUserLastSignIn =>
		"update_user_last_sign_in";
}
