// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;

namespace Jeebs.Data.Query.Exceptions;

/// <summary>
/// Thrown when <see cref="DbClient"/> is checking that a query predicate for the <see cref="Enums.Compare.In"/>
/// operator is a valid <see cref="IList"/>
/// </summary>
public class InvalidQueryPredicateException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public InvalidQueryPredicateException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public InvalidQueryPredicateException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public InvalidQueryPredicateException(string message, Exception inner) : base(message, inner) { }
}
