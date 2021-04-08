// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Default DB Connection Undefined
	/// </summary>
	public class DefaultDbConnectionUndefinedException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public DefaultDbConnectionUndefinedException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public DefaultDbConnectionUndefinedException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public DefaultDbConnectionUndefinedException(string message, Exception inner) : base(message, inner) { }
	}
}
