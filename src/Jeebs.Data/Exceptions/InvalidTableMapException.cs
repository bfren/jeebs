// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// See <see cref="Mapping.Mapper.Map{TEntity}(Mapping.ITable)"/>
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
