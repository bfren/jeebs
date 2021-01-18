using System;
using System.Collections.Generic;
using System.Text;
using static F.JsonF;

namespace Jx.Data
{
	/// <summary>
	/// UnitOfWork Exception
	/// </summary>
	public class UnitOfWorkException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public UnitOfWorkException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public UnitOfWorkException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="query">Query SQL</param>
		/// <param name="parameters">Query Parameters</param>
		/// <param name="inner">Inner Exception</param>
		public UnitOfWorkException(string query, object? parameters, Exception inner) : this($"Query: {query}; Parameters: {Serialise(parameters)}", inner) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public UnitOfWorkException(string message, Exception inner) : base(message, inner) { }
	}
}
