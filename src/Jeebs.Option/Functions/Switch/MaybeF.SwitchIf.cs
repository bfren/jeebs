// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using Jeebs.Internals;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// If the input <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/>, runs <paramref name="check"/> function -<br/>
	/// if it returns true and <paramref name="ifTrue"/> is not null, <paramref name="ifTrue"/> is returned,<br/>
	/// if it returns false and <paramref name="ifFalse"/> is not null, <paramref name="ifFalse"/> is returned,<br/>
	/// otherwise, the original <paramref name="maybe"/> is returned.
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="check">Function to run if <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/></param>
	/// <param name="ifTrue">Function to run if <paramref name="check"/> returns true</param>
	/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
	public static Maybe<T> SwitchIf<T>(
		Maybe<T> maybe,
		Func<T, bool> check,
		Func<T, Maybe<T>>? ifTrue,
		Func<T, Maybe<T>>? ifFalse
	)
	{
		if (maybe is Some<T> x)
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
				return None<T>(new M.SwitchIfFuncExceptionMsg(e));
			}
		}
		else if (maybe is None<T> y)
		{
			return y;
		}
		else
		{
			throw new UnknownMaybeException(); // as Maybe<T> is internal implementation only this should never happen...
		}
	}

	/// <summary>
	/// If the input <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/>, runs <paramref name="check"/> function -<br/>
	/// if it returns false, returns <see cref="Jeebs.Internals.None{T}"/> with the Reason from <paramref name="ifFalse"/>,<br/>
	/// otherwise, the original <paramref name="maybe"/> is returned.
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="check">Function to run if <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/></param>
	/// <param name="ifFalse">Function to run if <paramref name="check"/> returns false</param>
	public static Maybe<T> SwitchIf<T>(Maybe<T> maybe, Func<T, bool> check, Func<T, Msg> ifFalse) =>
		SwitchIf(maybe, check, null, x => None<T>(ifFalse(x)));

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>An exception was caught while executing one of the SwitchIf functions</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class SwitchIfFuncExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
