// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Return{T}(Func{T}, Handler?)"/>
		public static async Task<Option<T>> ReturnAsync<T>(Func<Task<T>> value, Handler handler)
		{
			try
			{
				return await value() switch
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

		/// <inheritdoc cref="Return{T}(Func{T}, bool, Handler)"/>
		public static async Task<Option<T?>> ReturnAsync<T>(Func<Task<T?>> value, bool allowNull, Handler handler)
		{
			try
			{
				var v = await value();

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
	}
}
