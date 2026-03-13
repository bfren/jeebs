// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Functions;
using Jeebs.Logging;
using Jeebs.Mvc.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth.Functions;

public static partial class AuthF
{
	/// <summary>
	/// Provides arguments for <see cref="DoSignInAsync(SignInArgs)"/>.
	/// </summary>
	/// <param name="Model">SignInModel.</param>
	/// <param name="Auth">IAuthDataProvider.</param>
	/// <param name="Log">ILog.</param>
	/// <param name="Url">IUrlHelper.</param>
	/// <param name="GetClaims">GetClaims delegate.</param>
	/// <param name="SignInAsync">Function to perform the signing in.</param>
	public sealed record class SignInArgs(
		SignInModel Model,
		IAuthDataProvider Auth,
		ILog Log,
		IUrlHelper Url,
		GetClaims? GetClaims,
		Func<ClaimsPrincipal, Task<AuthOp>> SignInAsync
	);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes.
	/// </summary>
	/// <param name="v">SignInArgs.</param>
	/// <returns>Auth operation result.</returns>
	public static Task<AuthOp> DoSignInAsync(SignInArgs v) =>
		DoSignInAsync(v, ValidateUserAsync);

	/// <summary>
	/// Perform sign in checks and do sign in if the user passes
	/// </summary>
	/// <param name="v">SignInArgs.</param>
	/// <param name="validate">Function to perform user validation.</param>
	/// <returns>Auth operation result.</returns>
	internal static async Task<AuthOp> DoSignInAsync(
		SignInArgs v,
		Func<IAuthDataProvider, SignInModel, ILog, Task<Result<AuthUserModel>>> validate
	)
	{
		// Validate user
		var result = await
			validate(
				v.Auth, v.Model, v.Log
			)
			.AuditAsync(
				fOk: x => v.Log.Dbg("User {UserId} validated.", x.Id.Value),
				fFail: v.Log.Failure
			);

		// Perform sign in
		if (result.Unsafe().TryOk(out var user))
		{
			// Get user principal			
			var principal = await GetPrincipalAsync(user, v.Model.Password, v.GetClaims);

			// Update last sign in
			ThreadF.FireAndForget(async () => await UpdateUserLastSignInAsync(v.Auth, user.Id, v.Log));

			// Sign in and return result
			return await v.SignInAsync(principal);
		}

		// Log error and add alert for user
		v.Log.Err("Unknown username or password: {Email}.", v.Model.Email);

		// Try again
		return new AuthOp.TryAgain();
	}
}
