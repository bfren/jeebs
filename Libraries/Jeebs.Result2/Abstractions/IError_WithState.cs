using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
    /// <summary>
	/// Result error, used when chain has a state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Chain state type</typeparam>
    public interface IError<TValue, TState> : IError<TValue>, IR<TValue, TState>
    {

    }
}
