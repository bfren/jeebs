// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Id;

/// <summary>
/// Object (Entity or Model) with ID property
/// </summary>
public interface IWithId
{
	/// <summary>
	/// IStrongId (wrapping a long value)
	/// </summary>
	IStrongId Id { get; }
}

/// <inheritdoc cref="IWithId"/>
/// <typeparam name="T">IStrongId Type</typeparam>
public interface IWithId<T> : IWithId
	where T : class, IStrongId, new()
{
	/// <summary>
	/// Strongly-typed IStrongId (wrapping a long value)
	/// </summary>
	new T Id { get; init; }

	/// <inheritdoc/>
	IStrongId IWithId.Id =>
		Id;
}
