// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Maybe;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// See <see cref="Map.Mapper.Map{TEntity}(Map.ITable)"/>
/// </summary>
public sealed class UnableToFindIdColumnException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnableToFindIdColumnException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="reason">Reason</param>
	public UnableToFindIdColumnException(IReason reason) : base(reason.ToString() ?? string.Empty) { }

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
