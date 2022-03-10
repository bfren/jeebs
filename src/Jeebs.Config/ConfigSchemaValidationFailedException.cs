// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Config;

/// <summary>
/// Configuration Schema Validation Failed
/// </summary>
public sealed class ConfigSchemaValidationFailedException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public ConfigSchemaValidationFailedException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public ConfigSchemaValidationFailedException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public ConfigSchemaValidationFailedException(string message, Exception inner) : base(message, inner) { }
}
