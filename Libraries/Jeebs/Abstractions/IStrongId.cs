// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	public interface IStrongId
	{
		/// <summary>
		/// ID Value as string
		/// </summary>
		string ValueStr { get; }

		/// <summary>
		/// Returns true if the current value is the default (i.e. unset) value
		/// </summary>
		bool IsDefault { get; }
	}

	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	/// <typeparam name="T">ID value type</typeparam>
	public interface IStrongId<T> : IStrongId
	{
		/// <summary>
		/// ID Value
		/// </summary>
		T Value { get; init; }
	}
}
