using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
    /// <summary>
	/// General result, used mainly for extension methods
	/// </summary>
    public interface IR : IDisposable
    {
        /// <summary>
		/// List of messages persisted through the result chain
		/// </summary>
        MsgList Messages { get; }

        /// <summary>
		/// Skip ahead instead of performing an action
		/// </summary>
        IError SkipAhead();

        /// <summary>
		/// Skip ahead instead of performing an action
		/// </summary>
		/// <typeparam name="TValue">Next result value type</typeparam>
        IError<TValue> SkipAhead<TValue>();
    }

    /// <summary>
	/// Main result
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
    public interface IR<TValue> : IR
    {
        /// <inheritdoc cref="IR.SkipAhead"/>
        new IError<TValue> SkipAhead();
    }
}