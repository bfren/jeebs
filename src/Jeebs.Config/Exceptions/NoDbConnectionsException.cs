// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config;

/// <summary>
/// No DB Connections
/// </summary>
public class NoDbConnectionsException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public NoDbConnectionsException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public NoDbConnectionsException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public NoDbConnectionsException(string message, Exception inner) : base(message, inner) { }
}
