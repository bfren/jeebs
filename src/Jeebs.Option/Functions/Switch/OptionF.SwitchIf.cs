// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// If the input <paramref name="option"/> is <see cref="Some{T}"/>, runs <paramref name="check"/> function -
		/// if that returns true, the original <paramref name="option"/> is returned, otherwise <paramref name="ifFalse"/>.
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Option being switched</param>
		/// <param name="check">Function to run if <paramref name="option"/> is <see cref="Some{T}"/></param>
		/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
		public static Option<T> SwitchIf<T>(Option<T> option, Func<T, bool> check, Func<T, Option<T>> ifFalse)
		{
			if (option is Some<T> x)
			{
				try
				{
					if (check(x.Value))
					{
						return x;
					}
					else
					{
						return ifFalse(x.Value);
					}
				}
				catch (Exception e)
				{
					return None<T>(new Msg.SwitchIfFuncExceptionMsg(e));
				}
			}
			else if (option is None<T> y)
			{
				return y;
			}
			else
			{
				throw new UnknownOptionException(); // as Option<T> is internal implementation only this should never happen...
			}
		}

		/// <inheritdoc cref="SwitchIf{T}(Option{T}, Func{T, bool}, Func{T, Option{T}})"/>
		public static Option<T> SwitchIf<T>(Option<T> option, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
			SwitchIf(option, check, x => None<T>(ifFalse(x)));

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>An exception was caught while executing one of the SwitchIf functions</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record SwitchIfFuncExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
