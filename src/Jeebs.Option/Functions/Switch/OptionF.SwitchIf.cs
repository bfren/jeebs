// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using Jeebs.Internals;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// If the input <paramref name="option"/> is <see cref="Jeebs.Internals.Some{T}"/>, runs <paramref name="check"/> function -<br/>
	/// if it returns true and <paramref name="ifTrue"/> is not null, <paramref name="ifTrue"/> is returned,<br/>
	/// if it returns false and <paramref name="ifFalse"/> is not null, <paramref name="ifFalse"/> is returned,<br/>
	/// otherwise, the original <paramref name="option"/> is returned.
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="option">Option being switched</param>
	/// <param name="check">Function to run if <paramref name="option"/> is <see cref="Jeebs.Internals.Some{T}"/></param>
	/// <param name="ifTrue">Function to run if <paramref name="check"/> returns true</param>
	/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
	public static Option<T> SwitchIf<T>(
		Option<T> option,
		Func<T, bool> check,
		Func<T, Option<T>>? ifTrue,
		Func<T, Option<T>>? ifFalse
	)
	{
		if (option is Some<T> x)
		{
			try
			{
				if (check(x.Value))
				{
					return ifTrue?.Invoke(x.Value) ?? x;
				}
				else
				{
					return ifFalse?.Invoke(x.Value) ?? x;
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

	/// <summary>
	/// If the input <paramref name="option"/> is <see cref="Jeebs.Internals.Some{T}"/>, runs <paramref name="check"/> function -<br/>
	/// if it returns false, returns <see cref="Jeebs.Internals.None{T}"/> with the Reason from <paramref name="ifFalse"/>,<br/>
	/// otherwise, the original <paramref name="option"/> is returned.
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="option">Option being switched</param>
	/// <param name="check">Function to run if <paramref name="option"/> is <see cref="Jeebs.Internals.Some{T}"/></param>
	/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
	public static Option<T> SwitchIf<T>(Option<T> option, Func<T, bool> check, Func<T, IMsg> ifFalse) =>
		SwitchIf(option, check, null, x => None<T>(ifFalse(x)));

	/// <summary>Messages</summary>
	public static partial class Msg
	{
		/// <summary>An exception was caught while executing one of the SwitchIf functions</summary>
		/// <param name="Exception">Exception object</param>
		public sealed record class SwitchIfFuncExceptionMsg(Exception Exception) : IExceptionMsg { }
	}
}
