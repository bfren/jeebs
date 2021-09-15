// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Enums;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// Thrown when an unrecognised <see cref="Compare"/> is found
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
	/// <param name="compare">Compare</param>
	public UnrecognisedSearchOperatorException(Compare compare) : this($"Unrecognised comparison: '{compare}'.") { }

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
