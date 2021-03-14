// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
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
}
