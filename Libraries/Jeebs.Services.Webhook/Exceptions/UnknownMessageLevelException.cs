// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jx.Services.Webhook
{
	/// <summary>
	/// Unknown MessageLevel
	/// </summary>
	public class UnknownMessageLevelException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownMessageLevelException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnknownMessageLevelException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnknownMessageLevelException(string message, Exception inner) : base(message, inner) { }
	}
}
