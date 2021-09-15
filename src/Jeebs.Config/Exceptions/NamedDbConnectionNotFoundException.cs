// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Config;

/// <summary>
/// Named DB Connection Not Found
/// </summary>
public class NamedDbConnectionNotFoundException : Exception
{
	/// <summary>
	/// Exception message format
	/// </summary>
	public const string Format = "Connection '{0}' was not found in configuration settings.";

	/// <summary>
	/// Create exception
	/// </summary>
	public NamedDbConnectionNotFoundException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="connection"></param>
	public NamedDbConnectionNotFoundException(string connection) : base(string.Format(Format, connection)) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public NamedDbConnectionNotFoundException(string message, Exception inner) : base(message, inner) { }
}
