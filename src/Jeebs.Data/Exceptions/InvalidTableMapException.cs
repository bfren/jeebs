// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// See <see cref="Map.EntityMapper.Map{TEntity, TTable}(TTable)"/>.
/// </summary>
public sealed class InvalidTableMapException : Exception
{
	/// <summary>
	/// List of errors showing why table map is invalid.
	/// </summary>
	public List<FailValue> Errors { get; init; }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="errors">List of errors.</param>
	public InvalidTableMapException(List<FailValue> errors) =>
		Errors = errors;
}
