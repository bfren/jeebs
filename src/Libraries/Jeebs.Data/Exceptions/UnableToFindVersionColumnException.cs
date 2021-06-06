// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// See <see cref="Mapper.Map{TEntity}(ITable)"/>
	/// </summary>
	public sealed class UnableToFindVersionColumnException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToFindVersionColumnException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="reason">Reason</param>
		public UnableToFindVersionColumnException(IMsg reason) : base(reason.ToString() ?? string.Empty) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnableToFindVersionColumnException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnableToFindVersionColumnException(string message, Exception inner) : base(message, inner) { }
	}
}
