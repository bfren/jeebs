// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="MapService.Map{TEntity}(Table)"/>
	/// </summary>
	public class InvalidTableMapException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidTableMapException() { }

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
