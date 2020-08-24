using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Result, used when result has state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Result state type</typeparam>
	public interface IR<TValue, TState> : IR<TValue>
	{
		/// <summary>
		/// Result state - persisted between actions, can be added via <see cref="IOk.WithState{TState}(TState)"/>
		/// </summary>
		TState State { get; }

		/// <summary>
		/// Add a log to this result
		/// </summary>
		/// <param name="log">ILog</param>
		new IR<TValue, TState> AddLogger(ILog log);

		/// <summary>
		/// Return an <see cref="IError{TValue}"/> with the current value and state types
		/// </summary>
		new IError<TValue, TState> Error();

		/// <summary>
		/// Return an <see cref="IError{TValue, TState}"/> with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		new IError<TNext, TState> Error<TNext>();

		/// <summary>
		/// Shorthand to return a 'false' result
		/// </summary>
		/// <param name="message">[Optional] Message to add</param>
		new IError<bool, TState> False(IMsg? message = null);
	}
}