// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jx.Services
{
	/// <summary>
	/// Unknown Driver
	/// </summary>
	public class UnknownDriverException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownDriverException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnknownDriverException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnknownDriverException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="t"></param>
		public UnknownDriverException(Type t) : this(t.ToString()) { }
	}
}
