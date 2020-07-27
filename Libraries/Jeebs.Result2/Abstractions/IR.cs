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
		/// Return a simple <see cref="IError"/> result
		/// </summary>
		IError Error();

		/// <summary>
		/// Return an <see cref="IError{TNext}"/> result with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		IError<TNext> Error<TNext>();
	}

	/// <summary>
	/// Main result
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public interface IR<TValue> : IR
	{
		/// <inheritdoc cref="IR.Error"/>
		new IError<TValue> Error();
	}
}