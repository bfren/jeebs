// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Result success with a value
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public interface IOkV<TValue> : IOk<TValue>
	{
		/// <summary>
		/// Result value
		/// </summary>
		TValue Value { get; }

		/// <inheritdoc cref="IOk{TValue}.WithState{TState}(TState)"/>
		new IOkV<TValue, TState> WithState<TState>(TState state);
	}
}
