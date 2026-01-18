// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// See <see cref="Map.EntityMapper.Map{TEntity, TTable}(TTable)"/>.
/// </summary>
public sealed class UnableToFindVersionColumnException : Exception
{
	/// <summary>
	/// Create exception.
	/// </summary>
	public UnableToFindVersionColumnException() { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="msg"></param>
	public UnableToFindVersionColumnException(IMsg msg) : base(msg.ToString() ?? string.Empty) { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	public UnableToFindVersionColumnException(string message) : base(message) { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnableToFindVersionColumnException(string message, Exception inner) : base(message, inner) { }
}
