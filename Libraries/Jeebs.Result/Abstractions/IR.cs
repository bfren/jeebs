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
		/// List of messages persisted from result to result
		/// </summary>
		MsgList Messages { get; }

		/// <summary>
		/// Returns true if this result contains messages
		/// </summary>
		bool HasMessages => Messages.Count > 0;

		/// <summary>
		/// Used to log events (e.g. exceptions) - requires <see cref="AddLogger(ILog)"/> to be called to set an application-specific log
		/// </summary>
		Logger Log { get; }

		/// <summary>
		/// Add a log to this result
		/// </summary>
		/// <param name="log">ILog</param>
		IR AddLogger(ILog log);

		/// <summary>
		/// Return a simple <see cref="IError"/> result
		/// </summary>
		IError Error();

		/// <summary>
		/// Return an <see cref="IError{TValue}"/> result with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		IError<TNext> Error<TNext>();

		/// <summary>
		/// Shorthand to return a 'false' result
		/// </summary>
		/// <param name="message">[Optional] Message to add</param>
		IError<bool> False(IMsg? message = null);
	}

	/// <summary>
	/// Main result
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public interface IR<TValue> : IR
	{
		/// <summary>
		/// Add a log to this result
		/// </summary>
		/// <param name="log">ILog</param>
		new IR<TValue> AddLogger(ILog log);

		/// <summary>
		/// Return an <see cref="IError{TValue}"/> with the current value type
		/// </summary>
		new IError<TValue> Error();
	}
}