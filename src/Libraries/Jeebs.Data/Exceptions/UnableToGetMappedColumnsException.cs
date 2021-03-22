// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// Unable to get mapped columns for specified entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public sealed class UnableToGetMappedColumnsException<TEntity> : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToGetMappedColumnsException() { }

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
