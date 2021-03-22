// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data.Mapping.Exceptions
{
	/// <summary>
	/// Unable to find ID column on specified entity
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public sealed class UnableToFindIdColumnException<TEntity> : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnableToFindIdColumnException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnableToFindIdColumnException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnableToFindIdColumnException(string message, Exception inner) : base(message, inner) { }
	}
}
