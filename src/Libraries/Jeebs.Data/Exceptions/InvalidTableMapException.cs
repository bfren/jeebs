// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="IMapper.Map{TEntity}(ITable)"/>
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
