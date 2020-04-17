using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Util;

namespace Jx.Data
{
	/// <summary>
	/// Query Exception
	/// </summary>
	public class QueryException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public QueryException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public QueryException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="query">Query SQL</param>
		/// <param name="parameters">Query Parameters</param>
		/// <param name="inner">Inner Exception</param>
		public QueryException(string query, object? parameters, Exception inner)
			: this($"Query: {query}; Parameters: {Json.Serialise(parameters)}", inner) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public QueryException(string message, Exception inner) : base(message, inner) { }
	}
}
