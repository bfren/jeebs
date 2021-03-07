// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Result success, used when result has a state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Result state type</typeparam>
	public interface IOk<TValue, TState> : IOk<TValue>, IR<TValue, TState>
	{
		/// <summary>
		/// Return an <see cref="IOk{TValue, TState}"/> result with the current value type
		/// </summary>
		new IOk<TValue, TState> Ok();

		/// <summary>
		/// Return an <see cref="IOk{TValue, TState}"/> result with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		new IOk<TNext, TState> Ok<TNext>();

		/// <summary>
		/// Return an <see cref="IOkV{TValue, TState}"/> result
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="value">Result value</param>
		new IOkV<TNext, TState> OkV<TNext>(TNext value);

		/// <summary>
		/// Shorthand to return a 'true' result
		/// </summary>
		/// <param name="message">[Optional] Message to add</param>
		new IOkV<bool, TState> OkTrue(IMsg? message = null);

		/// <summary>
		/// Shorthand to return a 'false' <see cref="IOk{TValue}"/> result
		/// </summary>
		/// <param name="message">[Optional] Message to add</param>
		new IOkV<bool, TState> OkFalse(IMsg? message = null);

		/// <summary>
		/// Return an <see cref="IError{TValue, TState}"/> result with the current value type
		/// </summary>
		new IError<TValue, TState> Error();

		/// <summary>
		/// Return an <see cref="IError{TNext, TState}"/> result with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		new IError<TNext, TState> Error<TNext>();
	}
}
