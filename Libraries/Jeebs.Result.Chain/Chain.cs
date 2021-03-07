// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		public static IOk<bool> Create() =>
			Chain<bool>.Create();

		/// <summary>
		/// Begin a chain with state (result value type will be <see cref="bool"/>)
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static IOk<bool, TState> Create<TState>(TState state) =>
			Chain<bool>.Create(state);

		/// <summary>
		/// Begin a chain with a value
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="value">Result value</param>
		public static IOkV<TValue> CreateV<TValue>(TValue value) =>
			Result.OkV(value);

		/// <summary>
		/// Begin a chain with a value and state
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="value">Result value</param>
		/// <param name="state">Chain state</param>
		public static IOkV<TValue, TState> CreateV<TValue, TState>(TValue value, TState state) =>
			Result.OkV(value, state);
	}

	/// <summary>
	/// Start a new result chain
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	public static class Chain<TValue>
	{
		/// <summary>
		/// Begin a chain
		/// </summary>
		public static IOk<TValue> Create() =>
			Result.Ok<TValue>();

		/// <summary>
		/// Begin a chain with state
		/// </summary>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="state">Chain state</param>
		public static IOk<TValue, TState> Create<TState>(TState state) =>
			Result.Ok<TValue, TState>(state);
	}
}