// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Config.Web.Auth;

/// <summary>
/// Attempting to add auth when auth is not enabled in configuration.
/// </summary>
public sealed class AuthNotEnabledException : Exception
{
	/// <inheritdoc/>
	public AuthNotEnabledException() : this("You need to enable auth in JSON configuration settings.") { }

	/// <inheritdoc/>
	public AuthNotEnabledException(string message) : base(message) { }

	/// <inheritdoc/>
	public AuthNotEnabledException(string message, Exception inner) : base(message, inner) { }
}
