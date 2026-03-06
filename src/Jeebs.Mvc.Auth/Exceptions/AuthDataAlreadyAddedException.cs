// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Mvc.Auth.Exceptions;

/// <summary>
/// See <see cref="AuthBuilder.WithData{TDbClient}(bool)"/>.
/// </summary>
public class AuthDataAlreadyAddedException : Exception
{
	/// <inheritdoc/>
	public AuthDataAlreadyAddedException() { }

	/// <inheritdoc/>
	public AuthDataAlreadyAddedException(string message) : base(message) { }

	/// <inheritdoc/>
	public AuthDataAlreadyAddedException(string message, Exception inner) : base(message, inner) { }
}
