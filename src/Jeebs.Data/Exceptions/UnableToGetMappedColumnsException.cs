// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// See <see cref="Mapper.Map{TEntity}(ITable)"/>
	/// </summary>
	public sealed class UnableToGetMappedColumnsException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToGetMappedColumnsException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="reason">Reason</param>
		public UnableToGetMappedColumnsException(IMsg reason) : base(reason.ToString() ?? string.Empty) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnableToGetMappedColumnsException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnableToGetMappedColumnsException(string message, Exception inner) : base(message, inner) { }
	}
}
