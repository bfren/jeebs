// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Exceptions
{
	/// <summary>
	/// Thrown when an unrecognised <see cref="SearchOperator"/> is found
	/// </summary>
	public sealed class UnrecognisedSearchOperatorException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnrecognisedSearchOperatorException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="operator">SearchOperator</param>
		public UnrecognisedSearchOperatorException(SearchOperator @operator) : this($"Unrecognised search operator: '{@operator}'.") { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnrecognisedSearchOperatorException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnrecognisedSearchOperatorException(string message, Exception inner) : base(message, inner) { }
	}
}
