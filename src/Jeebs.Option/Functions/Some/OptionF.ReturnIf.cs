// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create <see cref="Jeebs.Internals.Some{T}"/> with <paramref name="value"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="Jeebs.Internals.None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		/// <param name="handler">Exception handler</param>
		public static Option<T> SomeIf<T>(Func<bool> predicate, Func<T> value, Handler handler) =>
			Catch(() =>
				predicate() switch
				{
					true =>
						Some(value, handler),

					false =>
						None<T, Msg.PredicateWasFalseMsg>()
				},
				handler
			);

		/// <inheritdoc cref="SomeIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> SomeIf<T>(Func<bool> predicate, T value, Handler handler) =>
			SomeIf(predicate, () => value, handler);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record class PredicateWasFalseMsg : IMsg { }
		}
	}
}
