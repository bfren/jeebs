// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create a <see cref="Some{T}"/> Option, containing a value
		/// <para>If <paramref name="value"/> is null, <see cref="Jeebs.None{T}"/> will be returned instead</para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		/// <param name="allowNull">If true, <see cref="Some{T}"/> will always be returned</param>
		public static Option<T> Return<T>(T value, bool allowNull = false) =>
			value switch
			{
				T x =>
					new Some<T>(x),

				_ =>
					allowNull switch
					{
						true =>
							new Some<T>(value),

						false =>
							None<T, OptionFMsg.SomeValueWasNullMsg>()
					}

			};

		/// <summary>
		/// Create <see cref="Some{T}"/> with <paramref name="value"/> if <paramref name="predicate"/> is true
		/// <para>Otherwise, will return <see cref="Jeebs.None{T}"/></para>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="predicate">Predicate to evaluate</param>
		/// <param name="value">Function to return value</param>
		public static Option<T> ReturnIf<T>(Func<bool> predicate, Func<T> value) =>
			predicate() switch
			{
				true =>
					Return(value()),

				false =>
					None<T, OptionFMsg.PredicateWasFalseMsg>()
			};

		/// <inheritdoc cref="ReturnIf{T}(Func{bool}, Func{T})"/>
		public static Option<T> ReturnIf<T>(Func<bool> predicate, T value) =>
			ReturnIf(predicate, () => value);
	}
}

namespace F.OptionFMsg
{
	/// <summary>
	/// Predicate was false
	/// See <see cref="OptionF.ReturnIf{T}(Func{bool}, Func{T})"/>
	/// </summary>
	public sealed record PredicateWasFalseMsg : IMsg { }

	/// <summary>
	/// Value was null
	/// See <see cref="OptionF.Return{T}(T, bool)"/>
	/// </summary>
	public sealed record SomeValueWasNullMsg : IMsg { }
}
