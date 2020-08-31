using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Config
{
	/// <summary>
	/// Unknown Service
	/// </summary>
	[Serializable]
	public class UnknownServiceException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownServiceException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnknownServiceException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnknownServiceException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		public UnknownServiceException(string name, Type type) : this($"Unknown service '{name}' in {type} collection.") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected UnknownServiceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
