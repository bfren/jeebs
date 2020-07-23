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

        /// <inheritdoc cref="IR.SkipAhead"/>
        new IError<TValue, TState> SkipAhead();

        /// <inheritdoc cref="IR.SkipAhead{TValue}"/>
        new IError<TNext, TState> SkipAhead<TNext>();
    }
}