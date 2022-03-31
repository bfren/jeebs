// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.Exceptions;

/// <summary>
/// See <see cref="Map.Mapper.Map{TEntity}(Map.ITable)"/>
/// </summary>
public sealed class UnableToGetMappedColumnsException : Exception
{
	/// <summary>
	/// Create exception
	/// </summary>
	public UnableToGetMappedColumnsException() { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="msg"></param>
	public UnableToGetMappedColumnsException(IMsg msg) : base(msg.ToString() ?? string.Empty) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	public UnableToGetMappedColumnsException(string message) : base(message) { }

	/// <summary>
	/// Create exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnableToGetMappedColumnsException(string message, Exception inner) : base(message, inner) { }
}
