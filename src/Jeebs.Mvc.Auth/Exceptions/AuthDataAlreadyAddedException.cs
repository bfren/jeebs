// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Mvc.Auth.Exceptions;

/// <summary>
/// See <see cref="AuthBuilder.WithData{TDbClient}(bool)"/>.
/// </summary>
public class AuthDataAlreadyAddedException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public AuthDataAlreadyAddedException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public AuthDataAlreadyAddedException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public AuthDataAlreadyAddedException(string message, Exception inner) : base(message, inner) { }
}
