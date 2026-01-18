// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using Microsoft.AspNetCore.Http;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// Various authentication results.
/// </summary>
public abstract record class AuthResult : Result<string>
{
	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="result">Result value</param>
	/// <param name="statusCode"></param>
	private AuthResult(Maybe<string> result, int statusCode) : base(result) =>
		StatusCode = statusCode;

	/// <summary>
	/// Access is denied.
	/// </summary>
	public sealed record class Denied : AuthResult
	{
		/// <inheritdoc cref="Denied"/>
		public Denied() : base(F.None<string, M.DeniedMsg>(), StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Additional MFA information is required.
	/// </summary>
	public sealed record class MfaRequired : AuthResult
	{
		/// <inheritdoc cref="MfaRequired"/>
		public MfaRequired() : base(F.None<string, M.MfaRequiredMsg>(), StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Credentials were valid and the user has been signed in.
	/// </summary>
	public sealed record class SignedIn : AuthResult
	{
		/// <inheritdoc cref="SignedIn"/>
		/// <param name="result">[Optional] Result - usually redirect URL or JWT</param>
		public SignedIn(string? result) : base(F.Some(result ?? string.Empty), StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// The user has been signed out.
	/// </summary>
	public sealed record class SignedOut : AuthResult
	{
		/// <inheritdoc cref="SignedOut"/>
		/// <param name="redirectTo">[Optional] Redirect to this page</param>
		public SignedOut(string? redirectTo) : base(F.Some(redirectTo ?? string.Empty), StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// Credentials are invalid / not recognised, and the user can try again.
	/// </summary>
	public sealed record class TryAgain : AuthResult
	{
		/// <inheritdoc cref="TryAgain"/>
		public TryAgain() : base(F.None<string, M.TryAgainMsg>(), StatusCodes.Status401Unauthorized) { }
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
