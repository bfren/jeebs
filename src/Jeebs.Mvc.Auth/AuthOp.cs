// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Jwt;
using Microsoft.AspNetCore.Http;

namespace Jeebs.Mvc.Auth;

/// <summary>
/// Various authentication operations.
/// </summary>
public abstract record class AuthOp : Op<string>
{
	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="result">Result value.</param>
	/// <param name="statusCode">HTTP Status Code.</param>
	private AuthOp(Result<string> result, int statusCode) : base(result) =>
		StatusCode = statusCode;

	/// <summary>
	/// Access is denied.
	/// </summary>
	public sealed record class Denied : AuthOp
	{
		/// <inheritdoc cref="Denied"/>
		public Denied() : base(R.Fail("Access denied."), StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Credentials were valid and the user has been signed in.
	/// </summary>
	public sealed record class SignedIn : AuthOp
	{
		/// <inheritdoc cref="SignedIn"/>
		/// <param name="result">JWT containing validated credentials.</param>
		public SignedIn(JsonWebToken result) : base(result.Value, StatusCodes.Status200OK) { }

		/// <inheritdoc cref="SignedIn"/>
		/// <param name="redirectTo">URL to redirect to.</param>
		public SignedIn(string? redirectTo) : base(redirectTo ?? string.Empty, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// The user has been signed out.
	/// </summary>
	public sealed record class SignedOut : AuthOp
	{
		/// <inheritdoc cref="SignedOut"/>
		/// <param name="redirectTo">[Optional] Redirect to this page.</param>
		public SignedOut(string? redirectTo) : base(redirectTo ?? string.Empty, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// Credentials are invalid / not recognised, and the user can try again.
	/// </summary>
	public sealed record class TryAgain : AuthOp
	{
		/// <inheritdoc cref="TryAgain"/>
		public TryAgain() : base(R.Fail("Try again."), StatusCodes.Status401Unauthorized) { }
	}
}
