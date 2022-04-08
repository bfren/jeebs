// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
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
	/// <param name="redirectTo">[Optional] Redirect URL</param>
	/// <param name="statusCode">HTTP status code</param>
	private AuthResult(string result, string? redirectTo, int statusCode) :
		base(new { data = new { result, redirectTo } }, JsonF.CopyOptions()) =>
		StatusCode = statusCode;

	/// <summary>
	/// Access is denied
	/// </summary>
	public sealed class Denied : AuthResult
	{
		public Denied() : base(nameof(Denied), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Additional MFA information is required
	/// </summary>
	public sealed class MfaRequired : AuthResult
	{
		public MfaRequired() : base(nameof(MfaRequired), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Credentials were valid and the user has been signed in
	/// </summary>
	public sealed class SignedIn : AuthResult
	{
		public SignedIn(string redirectTo) : base(nameof(SignedIn), redirectTo, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// The user has been signed out
	/// </summary>
	public sealed class SignedOut : AuthResult
	{
		public SignedOut(string redirectTo) : base(nameof(SignedOut), redirectTo, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// Credentials are invalid / not recognised, and the user can try again
	/// </summary>
	public sealed class TryAgain : AuthResult
	{
		public TryAgain(string redirectTo) : base(nameof(TryAgain), redirectTo, StatusCodes.Status401Unauthorized) { }
	}
}
