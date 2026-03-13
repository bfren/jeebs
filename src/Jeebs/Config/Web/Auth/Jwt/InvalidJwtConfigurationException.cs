// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Config.Web.Auth.Jwt;

/// <summary>
/// Invalid JWT configuration.
/// </summary>
public sealed class InvalidJwtConfigurationException : Exception
{
	/// <inheritdoc/>
	public InvalidJwtConfigurationException() { }

	/// <inheritdoc/>
	public InvalidJwtConfigurationException(string message) : base(message) { }

	/// <inheritdoc/>
	public InvalidJwtConfigurationException(string message, Exception inner) : base(message, inner) { }
}
