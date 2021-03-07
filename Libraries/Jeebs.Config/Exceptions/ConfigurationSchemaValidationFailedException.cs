// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Configuration Schema Validation Failed
	/// </summary>
	[Serializable]
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

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ConfigurationSchemaValidationFailedException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
