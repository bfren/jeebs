// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data;

/// <inheritdoc cref="IWithVersion{TId, TValue}"/>
public interface IWithVersion : IWithId
{
	/// <summary>
	/// Version.
	/// </summary>
	long Version { get; }
}

/// <summary>
/// Object (Entity or Model) with Version property.
/// </summary>
/// <typeparam name="TId">ID Type.</typeparam>
/// <typeparam name="TValue">ID Value type</typeparam>
public interface IWithVersion<TId, TValue> : IWithVersion, IWithId<TId, TValue>
	where TId : class, IId<TId, TValue>, new()
	where TValue : struct
{ }
