// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Invalid Service Configuration
	/// </summary>
	public class InvalidServiceConfigurationException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "Service configuration '{0}' in {1} collection is not valid.";

		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidServiceConfigurationException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public InvalidServiceConfigurationException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public InvalidServiceConfigurationException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		public InvalidServiceConfigurationException(string name, Type type) : this(string.Format(Format, name, type)) { }
	}
}
