// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// Thrown by <see cref="Db(Config.DbConnectionConfig, ILog, IDbClient, string)"/>
	/// </summary>
	public class UnableToConnectToDatabaseException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToConnectToDatabaseException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnableToConnectToDatabaseException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnableToConnectToDatabaseException(string message, Exception inner) : base(message, inner) { }
	}
}
