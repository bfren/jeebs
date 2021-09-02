// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

/// <summary>
/// Represents a strongly-typed ID
/// </summary>
public interface IStrongId
{
	/// <summary>
	/// ID value
	/// </summary>
	ulong Value { get; init; }
}
