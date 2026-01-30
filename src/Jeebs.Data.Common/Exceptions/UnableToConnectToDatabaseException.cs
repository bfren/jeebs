// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// Thrown by <see cref="Db(IDbClient, Config.Db.DbConnectionConfig, Logging.ILog)"/>.
/// </summary>
public sealed class UnableToConnectToDatabaseException : Exception
{
	/// <summary>
	/// Create exception.
	/// </summary>
	public UnableToConnectToDatabaseException() { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	public UnableToConnectToDatabaseException(string message) : base(message) { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnableToConnectToDatabaseException(string message, Exception inner) : base(message, inner) { }
}
