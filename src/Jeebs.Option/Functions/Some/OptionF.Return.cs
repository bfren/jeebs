﻿// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Jeebs.Internals;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Create a <see cref="Jeebs.Internals.Some{T}"/> Option, containing <paramref name="value"/><br/>
		/// If <paramref name="value"/> returns null, <see cref="Jeebs.Internals.None{T}"/> will be returned instead
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		/// <param name="handler">Exception handler</param>
		public static Option<T> Some<T>(Func<T> value, Handler handler)
		{
			try
			{
				return value() switch
				{
					T x =>
						new Some<T>(x), // Some<T> is only created by Return() functions and implicit operator

					_ =>
						None<T, Msg.NullValueMsg>()

				};
			}
			catch (Exception e)
			{
				return None<T>(handler(e));
			}
		}

		/// <inheritdoc cref="Some{T}(Func{T}, Handler)"/>
		public static Option<T> Some<T>(T value) =>
			Some(() => value, DefaultHandler);

		/// <summary>
		/// Create a <see cref="Jeebs.Internals.Some{T}"/> Option, containing <paramref name="value"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="value">Some value</param>
		/// <param name="allowNull">If true, <see cref="Jeebs.Internals.Some{T}"/> will always be returned whatever the value is</param>
		/// <param name="handler">Exception handler</param>
		public static Option<T?> Some<T>(Func<T?> value, bool allowNull, Handler handler)
		{
			try
			{
				var v = value();

				return v switch
				{
					T x =>
						new Some<T?>(x), // Some<T> is only created by Return() functions and implicit operator

					_ =>
						allowNull switch
						{
							true =>
								new Some<T?>(v), // Some<T> is only created by Return() functions and implicit operator

							false =>
								None<T?, Msg.AllowNullWasFalseMsg>()
						}

				};
			}
			catch (Exception e)
			{
				return None<T?>(handler(e));
			}
		}

		/// <inheritdoc cref="Some{T}(Func{T}, bool, Handler)"/>
		public static Option<T?> Some<T>(T? value, bool allowNull) =>
			Some(() => value, allowNull, DefaultHandler);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Value was null when trying to wrap using Return</summary>
			public sealed record class NullValueMsg : IMsg { }

			/// <summary>Allow null was set to false when trying to return null value</summary>
			public sealed record class AllowNullWasFalseMsg : IMsg { }
		}
	}
}