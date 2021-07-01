// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Configuration Schema Validation Failed
	/// </summary>
	public class ConfigurationSchemaValidationFailedException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public ConfigurationSchemaValidationFailedException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public ConfigurationSchemaValidationFailedException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public ConfigurationSchemaValidationFailedException(string message, Exception inner) : base(message, inner) { }
	}
}
