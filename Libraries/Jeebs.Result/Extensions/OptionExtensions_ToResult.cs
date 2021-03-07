// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Extension methods for <see cref="Option{T}"/> objects: ToResult
	/// </summary>
	public static class OptionExtensions_ToResult
	{
		/// <summary>
		/// Convert an Option to a Result
		/// <para><see cref="Some{T}"/> returns <see cref="IOkV{TValue}"/>, with <see cref="Some{T}.Value"/> as the value</para>
		/// <para><see cref="None{T}"/> returns <see cref="IError{TValue}"/></para>
		/// </summary>
		/// <typeparam name="TValue">Option / Result value type</typeparam>
		/// <param name="this">Option</param>
		public static IR<TValue> ToResult<TValue>(this Option<TValue> @this) =>
			@this switch
			{
				Some<TValue> x =>
					Result.OkV(x.Value),

				None<TValue> x when x.Reason is IMsg r =>
					Result.Error<TValue>().AddMsg(r),

				_ =>
					Result.Error<TValue>()
			};

		/// <summary>
		/// Convert an Option to a Result (with state)
		/// <para><see cref="Some{T}"/> returns <see cref="IOkV{TValue, TState}"/>, with <see cref="Some{T}.Value"/> as the value</para>
		/// <para><see cref="None{T}"/> returns <see cref="IError{TValue, TState}"/></para>
		/// </summary>
		/// <typeparam name="TValue">Option / Result value type</typeparam>
		/// <typeparam name="TState">Result state type</typeparam>
		/// <param name="this">Option</param>
		/// <param name="state">Result state</param>
		public static IR<TValue, TState> ToResult<TValue, TState>(this Option<TValue> @this, TState state) =>
			@this switch
			{
				Some<TValue> x =>
					Result.OkV(x.Value, state),

				None<TValue> x when x.Reason is IMsg r =>
					Result.Error<TValue, TState>(state).AddMsg(r),

				_ =>
					Result.Error<TValue, TState>(state)
			};
	}
}
