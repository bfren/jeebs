// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
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
		public static Option<T> Return<T>(T value)
		{
			try
			{
				return value switch
				{
					T x =>
						new Some<T>(x), // this is the only place Some<T> is created

					_ =>
						None<T, Msg.AllowNullWasFalseMsg>()

				};
			}
			catch (Exception e)
			{
				return None<T>(DefaultHandler(e));
			}
		}

		/// <summary>
		/// Create a <see cref="Some{T}"/> Option, containing <paramref name="value"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		/// <param name="allowNull">If true, <see cref="Some{T}"/> will always be returned</param>
		public static Option<T?> Return<T>(T? value, bool allowNull)
		{
			try
			{
				return value switch
				{
					T x =>
						new Some<T?>(x), // this is the only place Some<T> is created

					_ =>
						allowNull switch
						{
							true =>
								new Some<T?>(value), // this is the only place Some<T> is created

							false =>
								None<T?, Msg.AllowNullWasFalseMsg>()
						}

				};
			}
			catch (Exception e)
			{
				return None<T?>(DefaultHandler(e));
			}
		}

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Predicate was false</summary>
			public sealed record PredicateWasFalseMsg : IMsg { }

			/// <summary>Value was null when trying to wrap using Return</summary>
			public sealed record NullValueMsg : IMsg { }

			/// <summary>Allow null was set to false when trying to return null value</summary>
			public sealed record AllowNullWasFalseMsg : IMsg { }
		}
	}
}
