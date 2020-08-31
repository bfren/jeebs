using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Config
{
	/// <summary>
	/// Invalid Service Configuration
	/// </summary>
	[Serializable]
	public class InvalidServiceConfigurationException : Exception
	{
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
		public InvalidServiceConfigurationException(string name, Type type) : this($"Service configuration '{name}' in {type} collection is not valid.") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected InvalidServiceConfigurationException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
