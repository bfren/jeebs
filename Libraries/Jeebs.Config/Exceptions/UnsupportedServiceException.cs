using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Config
{
	/// <summary>
	/// Unsupported Service
	/// </summary>
	[Serializable]
	public class UnsupportedServiceException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnsupportedServiceException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnsupportedServiceException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnsupportedServiceException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="type"></param>
		public UnsupportedServiceException(Type type) : this($"Unsupported service type '{type}'.") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected UnsupportedServiceException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
