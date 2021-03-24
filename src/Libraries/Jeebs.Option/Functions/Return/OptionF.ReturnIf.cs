// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create <see cref="Some{T}"/> with <paramref name="value"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="Jeebs.None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		/// <param name="handler">Exception handler</param>
		public static Option<T> ReturnIf<T>(Func<bool> predicate, Func<T> value, Handler handler) =>
			Catch(() =>
				predicate() switch
				{
					true =>
						Return(value, handler),

					false =>
						None<T, Msg.PredicateWasFalseMsg>()
				},
				handler
			);

		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T}, Handler)"/>
		public static Option<T> ReturnIf<T>(Func<bool> predicate, T value, Handler handler) =>
			ReturnIf(predicate, () => value, handler);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record PredicateWasFalseMsg : IMsg { }
		}
	}
}
