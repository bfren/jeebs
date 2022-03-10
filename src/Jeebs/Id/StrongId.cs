// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Id;

/// <summary>
/// Represents a strongly-typed ID
/// </summary>
/// <param name="Value">ID value</param>
public abstract record class StrongId(long Value) : IStrongId
{
	/// <summary>
	/// A parameterless constructor is necessary for model binding
	/// </summary>
	protected StrongId() : this(0L) { }
}
