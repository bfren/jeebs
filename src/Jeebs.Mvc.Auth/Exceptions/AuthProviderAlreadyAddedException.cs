// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Mvc.Auth.Exceptions
{
	/// <summary>
	/// See <see cref="AuthBuilder.CheckProvider"/>
	/// </summary>
	public class AuthProviderAlreadyAddedException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public AuthProviderAlreadyAddedException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public AuthProviderAlreadyAddedException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public AuthProviderAlreadyAddedException(string message, Exception inner) : base(message, inner) { }
	}
}
