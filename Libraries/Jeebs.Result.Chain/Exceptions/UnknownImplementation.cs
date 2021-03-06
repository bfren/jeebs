// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs;

namespace Jx.Result
{
	/// <summary>
	/// Thrown when a custom implementation of <see cref="IR"/> is used.
	/// </summary>
	public class UnknownImplementationException : Exception
	{
		private readonly static string error = $"Unknown implementation of {typeof(IR)}.";

		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownImplementationException() : base(error) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public UnknownImplementationException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner Exception</param>
		public UnknownImplementationException(string message, Exception inner) : base(message, inner) { }
	}
}
