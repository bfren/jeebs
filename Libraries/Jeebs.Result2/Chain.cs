using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Start a new result chain
	/// </summary>
	public static class R
	{
		/// <summary>
		/// Begin a chain (result value type will be <see cref="bool"/>)
		/// </summary>
		public static IOk Chain() => new ROkSimple();

		/// <summary>
		/// Begin an async chain (result value type will be <see cref="bool"/>)
		/// </summary>
		public static Task<IOk> ChainAsync() => Task.FromResult(Chain());

		/// <summary>
		/// Begin a chain with state (result value type will be <see cref="bool"/>)
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static IOk<bool, TState> Chain<TState>(TState state) => new ROkSimple<TState>(state);

		/// <summary>
		/// Begin an async chain with state (result value type will be <see cref="bool"/>)
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static Task<IOk<bool, TState>> ChainAsync<TState>(TState state) => Task.FromResult(Chain(state));

		/// <summary>
		/// Begin a chain with a value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static IOkV<TValue> ChainV<TValue>(TValue value) => new ROkV<TValue>(value);

		/// <summary>
		/// Begin an async chain with a value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static Task<IOkV<TValue>> ChainVAsync<TValue>(TValue value) => Task.FromResult(ChainV(value));

		/// <summary>
		/// Begin a chain with a value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Chain state</param>
		public static IOkV<TValue, TState> ChainV<TValue, TState>(TValue value, TState state) => new ROkV<TValue, TState>(value, state);

		/// <summary>
		/// Begin an async chain with a value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Chain state</param>
		public static Task<IOkV<TValue, TState>> ChainVAsync<TValue, TState>(TValue value, TState state) => Task.FromResult(ChainV(value, state));
	}
}