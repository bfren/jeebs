﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Config
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
