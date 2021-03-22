// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Data.Mapping.Exceptions
{
	/// <summary>
	/// See <see cref="Mapper.Map{TEntity}(ITable)"/>
	/// </summary>
	public sealed class InvalidTableMapException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidTableMapException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="errors">List of errors</param>
		public InvalidTableMapException(List<string> errors) : base(string.Join("\n", errors)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public InvalidTableMapException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public InvalidTableMapException(string message, Exception inner) : base(message, inner) { }
	}
}
