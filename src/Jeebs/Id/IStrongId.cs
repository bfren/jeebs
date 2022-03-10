// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Id;

/// <summary>
/// Represents a strongly-typed ID
/// </summary>
public interface IStrongId
{
	/// <summary>
	/// ID value
	/// </summary>
	long Value { get; init; }
}
