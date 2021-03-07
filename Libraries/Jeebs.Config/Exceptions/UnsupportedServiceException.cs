// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Config
{
	/// <summary>
	/// Unsupported Service
	/// </summary>
	[Serializable]
	public class UnsupportedServiceException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "Unsupported service type '{0}'.";

		/// <summary>
		/// Create exception
		/// </summary>
		public UnsupportedServiceException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="type">Service type</param>
		public UnsupportedServiceException(string type) : base(string.Format(Format, type)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnsupportedServiceException(string message, Exception inner) : base(message, inner) { }

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
