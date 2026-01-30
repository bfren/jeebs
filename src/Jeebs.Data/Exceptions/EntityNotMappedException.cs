// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Data.QueryBuilder.Exceptions;

/// <summary>
/// Thrown when an entity is not mapped - see <see cref="QueryPartsBuilderWithEntity{TEntity, TId}.Map"/>
/// </summary>
public class EntityNotMappedException : Exception
{
	/// <summary>
	/// Create exception.
	/// </summary>
	public EntityNotMappedException() { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	public EntityNotMappedException(string message) : base(message) { }

	/// <summary>
	/// Create exception.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="inner"></param>
	public EntityNotMappedException(string message, Exception inner) : base(message, inner) { }
}
