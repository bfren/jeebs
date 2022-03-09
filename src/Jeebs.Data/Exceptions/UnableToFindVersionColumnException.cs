// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Maybe;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// See <see cref="Map.Mapper.Map{TEntity}(Map.ITable)"/>
/// </summary>
public sealed class UnableToFindVersionColumnException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnableToFindVersionColumnException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="reason">Reason</param>
	public UnableToFindVersionColumnException(IReason reason) : base(reason.ToString() ?? string.Empty) { }

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
