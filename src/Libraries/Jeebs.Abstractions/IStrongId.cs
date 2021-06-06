// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	public interface IStrongId
	{
		/// <summary>
		/// ID Value
		/// </summary>
		long Value { get; init; }

		/// <summary>
		/// Returns true if the current value is the default (i.e. unset) value
		/// </summary>
		bool IsDefault { get; }
	}
}
