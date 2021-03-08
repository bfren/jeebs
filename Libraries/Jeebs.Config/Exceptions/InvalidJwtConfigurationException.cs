// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Invalid JWT configuration
	/// </summary>
	public class InvalidJwtConfigurationException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidJwtConfigurationException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public InvalidJwtConfigurationException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public InvalidJwtConfigurationException(string message, Exception inner) : base(message, inner) { }
	}
}
