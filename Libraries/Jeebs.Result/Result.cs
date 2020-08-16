using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Create an OK result
	/// </summary>
	public static class Result
	{
		/// <summary>
		/// Ok result
		/// </summary>
		public static IOk Ok()
			=> new ROk<bool>();

		/// <summary>
		/// Ok result, with state
		/// </summary>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="state">Result state</param>
		public static IOk<bool, TState> Ok<TState>(TState state)
			=> new ROk<bool, TState>(state);

		/// <summary>
		/// Ok result
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		public static IOk<TValue> Ok<TValue>()
			=> new ROk<TValue>();

		/// <summary>
		/// Ok result, with state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="state">Result state</param>
		public static IOk<TValue, TState> Ok<TValue, TState>(TState state)
			=> new ROk<TValue, TState>(state);

		/// <summary>
		/// Ok result, with value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static IOkV<TValue> OkV<TValue>(TValue value)
			=> new ROkV<TValue>(value);

		/// <summary>
		/// Ok result, with value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Result state</param>
		public static IOkV<TValue, TState> OkV<TValue, TState>(TValue value, TState state)
			=> new ROkV<TValue, TState>(value, state);

		/// <summary>
		/// Result error
		/// </summary>
		public static IError Error()
			=> new RError<bool>();

		/// <summary>
		/// Result error
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		public static IError<TValue> Error<TValue>()
			=> new RError<TValue>();

		/// <summary>
		/// Result error, with state
		/// </summary>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="state">Result state</param>
		public static IError<bool, TState> Error<TState>(TState state)
			=> new RError<bool, TState>(state);

		/// <summary>
		/// Result error, with state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="state">Result state</param>
		public static IError<TValue, TState> Error<TValue, TState>(TState state)
			=> new RError<TValue, TState>(state);
	}
}