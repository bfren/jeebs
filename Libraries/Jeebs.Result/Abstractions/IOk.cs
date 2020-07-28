using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// General result success, used to pass chain as method parameters
	/// </summary>
	public interface IOk : IR
	{
		/// <summary>
		/// Return a simple <see cref="IOk"/> result
		/// </summary>
		IOk Ok();

		/// <summary>
		/// Return an <see cref="IOk{TValue}"/> result with a new value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		IOk<TNext> Ok<TNext>();

		/// <summary>
		/// Return an <see cref="IOkV{TValue}"/> result
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="value">Result value</param>
		IOkV<TNext> OkV<TNext>(TNext value);

		/// <summary>
		/// Shorthand to return a 'true' result
		/// </summary>
		/// <param name="message">[Optional] Message to add</param>
		IOk<bool> True(IMsg? message = null);
	}

	/// <summary>
	/// Main result success
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public interface IOk<TValue> : IOk, IR<TValue>
	{
		/// <summary>
		/// Return an <see cref="IOk{TValue}"/> result with the current value type
		/// </summary>
		new IOk<TValue> Ok();

		/// <summary>
		/// Add state to the result
		/// </summary>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="state">Result state</param>
		IOk<TValue, TState> WithState<TState>(TState state);
	}
}