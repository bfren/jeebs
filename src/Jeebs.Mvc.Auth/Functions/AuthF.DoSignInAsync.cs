// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Functions;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignInAsync(SignInArgs)"/>
	/// </summary>
	/// <param name="Model"></param>
	/// <param name="Auth"></param>
	/// <param name="Log"></param>
	/// <param name="Url"></param>
	/// <param name="GetClaims"></param>
	/// <param name="SignInAsync"></param>
	public sealed record class SignInArgs(
		SignInModel Model,
		IAuthDataProvider Auth,
		ILog Log,
		IUrlHelper Url,
		GetClaims? GetClaims,
		Func<ClaimsPrincipal, Task<AuthResult>> SignInAsync
	);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes
	/// </summary>
	/// <param name="v"></param>
	public static Task<AuthResult> DoSignInAsync(SignInArgs v) =>
		DoSignInAsync(v, ValidateUserAsync);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes
	/// </summary>
	/// <param name="v"></param>
	/// <param name="validate"></param>
	internal static async Task<AuthResult> DoSignInAsync(SignInArgs v, Func<IAuthDataProvider, SignInModel, ILog, Task<Maybe<AuthUserModel>>> validate)
	{
		// Validate user
		var validateResult = await
			validate(
				v.Auth, v.Model, v.Log
			)
			.AuditAsync(
				some: x => v.Log.Dbg("User {UserId} validated.", x.Id.Value),
				none: v.Log.Msg
			)
			.ConfigureAwait(false);

		// Perform sign in
		if (validateResult.IsSome(out var user))
		{
			// Get user principal			
			var principal = await GetPrincipalAsync(user, v.Model.Password, v.GetClaims).ConfigureAwait(false);

			// Update last sign in
			ThreadF.FireAndForget(() => UpdateUserLastSignInAsync(v.Auth, user.Id, v.Log));

			// Sign in and return result
			return await v.SignInAsync(principal);
		}

		// Log error and add alert for user
		v.Log.Err("Unknown username or password: {Email}.", v.Model.Email);

		// Try again
		return new AuthResult.TryAgain();
	}
}
