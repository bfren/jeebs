// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;

namespace Jeebs.Data.Querying.Exceptions
{
	/// <summary>
	/// Thrown when something goes wrong while building a query
	/// </summary>
	/// <typeparam name="TTable">Table type</typeparam>
	public abstract class QueryBuilderException<TTable> : Exception
		where TTable : ITable
	{
		/// <summary>
		/// Create exception
		/// </summary>
		protected QueryBuilderException() : this($"You need to add Table {typeof(TTable)} before using it in a query.") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		protected QueryBuilderException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		protected QueryBuilderException(string message, Exception inner) : base(message, inner) { }
	}
}
