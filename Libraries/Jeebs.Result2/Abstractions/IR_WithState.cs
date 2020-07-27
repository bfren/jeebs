using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
    /// <summary>
	/// Result, used when chain has a state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Chain state type</typeparam>
    public interface IR<TValue, TState> : IR<TValue>
    {
        /// <summary>
		/// Chain state (created only in <see cref="R"/>, and does not change throughout the result chain)
		/// </summary>
        TState State { get; }

        /// <inheritdoc cref="IR.Error"/>
        new IError<TValue, TState> Error();

        /// <inheritdoc cref="IR.Error{TValue}"/>
        new IError<TNext, TState> Error<TNext>();
    }
}