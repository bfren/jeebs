using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Config
{
	/// <summary>
	/// Invalid Service Definition
	/// </summary>
	[Serializable]
	public class InvalidServiceDefinitionException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidServiceDefinitionException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="definition"></param>
		public InvalidServiceDefinitionException(string definition) : base($"Invalid service definition '{definition}': should be [service_type].[service_name].") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public InvalidServiceDefinitionException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected InvalidServiceDefinitionException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
