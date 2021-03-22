// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// Unable to find Version column on specified entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public sealed class UnableToFindVersionColumnException<TEntity> : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToFindVersionColumnException() { }

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
