using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Start a new result chain
	/// </summary>
	public static class Chain
	{
		/// <summary>
		/// Begin a chain (result value type will be <see cref="bool"/>)
		/// </summary>
		public static IOk<bool> Create() => R.Ok();

		/// <summary>
		/// Begin an async chain (result value type will be <see cref="bool"/>)
		/// </summary>
		public static Task<IOk<bool>> CreateAsync() => Task.FromResult(Create());

		/// <summary>
		/// Begin a chain with state (result value type will be <see cref="bool"/>)
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static IOk<bool, TState> Create<TState>(TState state) => R.Ok<bool, TState>(state);

		/// <summary>
		/// Begin an async chain with state (result value type will be <see cref="bool"/>)
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static Task<IOk<bool, TState>> CreateAsync<TState>(TState state) => Task.FromResult(Create(state));

		/// <summary>
		/// Begin a chain with a value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static IOkV<TValue> CreateV<TValue>(TValue value) => R.OkV(value);

		/// <summary>
		/// Begin an async chain with a value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static Task<IOkV<TValue>> CreateVAsync<TValue>(TValue value) => Task.FromResult(CreateV(value));

		/// <summary>
		/// Begin a chain with a value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Chain state</param>
		public static IOkV<TValue, TState> CreateV<TValue, TState>(TValue value, TState state) => R.OkV(value, state);

		/// <summary>
		/// Begin an async chain with a value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Chain state</param>
		public static Task<IOkV<TValue, TState>> CreateVAsync<TValue, TState>(TValue value, TState state) => Task.FromResult(CreateV(value, state));
	}
}