// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.Fluent
{
	/// <summary>
	/// Allows result type changing
	/// </summary>
	public sealed class ChangeType
	{
		private readonly IR result;

		internal ChangeType(IR result) =>
			this.result = result;

		/// <summary>
		/// Change result to use <typeparamref name="TNext"/> as the result value type
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		public IR<TNext> To<TNext>() =>
			result switch
			{
				IOk x =>
					x.Ok<TNext>(),

				IError x =>
					x.Error<TNext>(),

				_ =>
					throw new InvalidOperationException($"{result.GetType()} is not a supported implementation of {typeof(IR)}.")
			};
	}

	/// <summary>
	/// Allows result type changing
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Result chain type</typeparam>
	public sealed class ChangeType<TValue, TState>
	{
		private readonly IR<TValue, TState> result;

		internal ChangeType(IR<TValue, TState> result) =>
			this.result = result;

		/// <inheritdoc cref="ChangeType.To{TNext}"/>
		public IR<TNext, TState> To<TNext>() =>
			result switch
			{
				IOk<TValue, TState> x =>
					x.Ok<TNext>(),

				IError<TValue, TState> x =>
					x.Error<TNext>(),

				_ =>
					throw new InvalidOperationException($"{result.GetType()} is not a supported implementation of {typeof(IR<,>)}.")
			};
	}
}
