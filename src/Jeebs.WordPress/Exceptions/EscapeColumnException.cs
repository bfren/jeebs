// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.WordPress.Query;

namespace Jeebs.WordPress.Exceptions;

/// <summary>
/// Thrown when <see cref="PartsBuilder{TId}"/> fails to escape a column.
/// </summary>
public sealed class EscapeColumnException : Exception
{
	/// <summary>
	/// Create exception with the specified Failure.
	/// </summary>
	/// <param name="failure">Failure object.</param>
	public EscapeColumnException(Failure failure) : base($"Error escaping column: {failure}") { }
}
