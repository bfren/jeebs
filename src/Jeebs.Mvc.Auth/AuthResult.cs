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
	/// <param name="message"></param>
	/// <param name="redirectTo"></param>
	/// <param name="statusCode"></param>
	private AuthResult(bool result, string message, string? redirectTo, int statusCode) : base(
		Result.Create<bool>(result, message) with { RedirectTo = redirectTo },
		JsonF.CopyOptions()
	) =>
		StatusCode = statusCode;

	/// <summary>
	/// Access is denied
	/// </summary>
	public sealed class Denied : AuthResult
	{
		/// <summary>
		/// Create object
		/// </summary>
		public Denied() : base(false, nameof(Denied), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Additional MFA information is required
	/// </summary>
	public sealed class MfaRequired : AuthResult
	{
		/// <summary>
		/// Create object
		/// </summary>
		public MfaRequired() : base(false, nameof(MfaRequired), null, StatusCodes.Status401Unauthorized) { }
	}

	/// <summary>
	/// Credentials were valid and the user has been signed in
	/// </summary>
	public sealed class SignedIn : AuthResult
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="redirectTo"></param>
		public SignedIn(string? redirectTo) : base(true, nameof(SignedIn), redirectTo, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// The user has been signed out
	/// </summary>
	public sealed class SignedOut : AuthResult
	{
		/// <summary>
		/// Create object
		/// </summary>
		public SignedOut() : base(true, nameof(SignedOut), null, StatusCodes.Status200OK) { }
	}

	/// <summary>
	/// Credentials are invalid / not recognised, and the user can try again
	/// </summary>
	public sealed class TryAgain : AuthResult
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="redirectTo"></param>
		public TryAgain(string? redirectTo) : base(false, nameof(TryAgain), redirectTo, StatusCodes.Status401Unauthorized) { }
	}
}
