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
		/// Return an <see cref="IError{TValue}"/> with the current value and state types
		/// </summary>
		new IError<TValue, TState> Error();

		/// <summary>
		/// Return an <see cref="IError{TValue, TState}"/> with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		new IError<TNext, TState> Error<TNext>();

		/// <summary>
		/// Type-safe switching - runs <paramref name="okV"/> when current object is <see cref="IOkV{TValue, TState}"/>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="okV">Runs when current object is <see cref="IOkV{TValue, TState}"/></param>
		/// <param name="other">[Optional] Runs when current object is not - if not supplied will return <see cref="IError{TNext, TState}"/></param>
		IR<TNext, TState> Switch<TNext>(Func<IOkV<TValue>, IR<TNext, TState>> okV, Func<IR<TValue, TState>, IR<TNext, TState>>? other = null);
	}
}