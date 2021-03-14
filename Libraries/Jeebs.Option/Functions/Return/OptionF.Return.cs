// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create a <see cref="Some{T}"/> Option, containing <paramref name="value"/><br/>
		/// If <paramref name="value"/> is null, <see cref="Jeebs.None{T}"/> will be returned instead
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
							None<T, Msg.SomeValueWasNullMsg>()
					}

			};

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record PredicateWasFalseMsg : IMsg { }

			/// <summary>Value was null when trying to wrap using Return</summary>
			public sealed record SomeValueWasNullMsg : IMsg { }
		}
	}
}
