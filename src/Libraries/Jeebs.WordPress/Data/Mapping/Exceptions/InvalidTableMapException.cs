// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.WordPress.Data.Mapping;

namespace Jx.Data.Mapping
{
	/// <summary>
	/// See <see cref="IMapService.Map{TEntity}(ITable)"/>
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
