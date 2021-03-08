// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <summary>
	/// General result, used mainly for extension methods
	/// </summary>
	public interface IR
	{
		/// <summary>
		/// List of messages persisted from result to result
		/// </summary>
		IMsgList Messages { get; }

		/// <summary>
		/// Returns true if this result contains messages
		/// </summary>
		bool HasMessages =>
			Messages.Count > 0;

		/// <summary>
		/// Used to log events (e.g. exceptions)
		/// </summary>
		ILogger Logger { get; }

		/// <summary>
		/// Return a simple <see cref="IError"/> result
		/// </summary>
		IError Error();

		/// <summary>
		/// Return an <see cref="IError{TValue}"/> result with a new value type
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
		/// <summary>
		/// Return an <see cref="IError{TValue}"/> with the current value type
		/// </summary>
		new IError<TValue> Error();

		/// <summary>
		/// Type-safe switching - runs <paramref name="okV"/> when current object is <see cref="IOkV{TValue}"/>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="okV">Runs when current object is <see cref="IOkV{TValue}"/></param>
		/// <param name="other">[Optional] Runs when current object is not - if not supplied will return <see cref="IError{TNext}"/></param>
		IR<TNext> Switch<TNext>(Func<IOkV<TValue>, IR<TNext>> okV, Func<IR<TValue>, IR<TNext>>? other = null);
	}
}