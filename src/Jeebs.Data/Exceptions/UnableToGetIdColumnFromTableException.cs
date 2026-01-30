// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.Exceptions;

/// <summary>
/// See <see cref="QueryPartsBuilder.GetIdColumn{T}(T)"/>
/// </summary>
public sealed class UnableToGetIdColumnFromTableException : Exception
{
	/// <summary>
	/// Create object
	/// </summary>
	public UnableToGetIdColumnFromTableException() { }

	/// <summary>
	/// Create object with table
	/// </summary>
	/// <param name="table"></param>
	public UnableToGetIdColumnFromTableException(ITable table) : this(table.GetName().Name) { }

	/// <summary>
	/// Create object with message
	/// </summary>
	/// <param name="message"></param>
	public UnableToGetIdColumnFromTableException(string message) : base(message) { }

	/// <summary>
	/// Create object with message and exception
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public UnableToGetIdColumnFromTableException(string message, Exception inner) : base(message, inner) { }
}
