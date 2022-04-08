// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
using Jeebs.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// Various authentication results
/// </summary>
public abstract class AuthResult : JsonResult
{
	/// <summary>
	/// Create JsonResult using the specified options
	/// </summary>
	/// <param name="result">Result value</param>
	/// <param name="redirectTo"></param>
	/// <param name="statusCode"></param>
	private AuthResult(Maybe<bool> result, string? redirectTo, int statusCode) : base(
		Result.Create(result) with { RedirectTo = redirectTo },
		JsonF.CopyOptions()
	) =>
		StatusCode = statusCode;

	/// <summary>
	/// Access is denied
	/// </summary>
	public sealed class Denied : AuthResult
	{
		/// <inheritdoc cref="Denied"/>
		public Denied() : base(F.None<bool, M.DeniedMsg>(), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Additional MFA information is required
	/// </summary>
	public sealed class MfaRequired : AuthResult
	{
		/// <inheritdoc cref="MfaRequired"/>
		public MfaRequired() : base(F.None<bool, M.MfaRequiredMsg>(), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Credentials were valid and the user has been signed in
	/// </summary>
	public sealed class SignedIn : AuthResult
	{
		/// <inheritdoc cref="SignedIn"/>
		/// <param name="redirectTo">[Optional] Redirect to this page</param>
		public SignedIn(string? redirectTo) : base(F.True, redirectTo, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// The user has been signed out
	/// </summary>
	public sealed class SignedOut : AuthResult
	{
		/// <inheritdoc cref="SignedOut"/>
		public SignedOut() : base(F.True, null, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// Credentials are invalid / not recognised, and the user can try again
	/// </summary>
	public sealed class TryAgain : AuthResult
	{
		/// <inheritdoc cref="TryAgain"/>
		/// <param name="redirectTo">[Optional] Redirect to this page</param>
		public TryAgain(string? redirectTo) : base(F.None<bool, M.TryAgainMsg>(), redirectTo, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>Messages</summary>
	public static class M
	{
		/// <inheritdoc cref="Denied"/>
		public sealed record class DeniedMsg : Msg;

		/// <inheritdoc cref="MfaRequired"/>
		public sealed record class MfaRequiredMsg : Msg;

		/// <inheritdoc cref="TryAgainMsg"/>
		public sealed record class TryAgainMsg : Msg;
	}
}
