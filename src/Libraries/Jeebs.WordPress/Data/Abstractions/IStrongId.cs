// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	/// <typeparam name="T">ID value type</typeparam>
	public interface IStrongId<T>
	{
		/// <summary>
		/// ID Value
		/// </summary>
		T Value { get; init; }
	}
}