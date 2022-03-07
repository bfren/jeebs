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
	/// Run an action depending on whether <paramref name="maybe"/> is a <see cref="Jeebs.Internals.Some{T}"/> or <see cref="Jeebs.Internals.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Action to run if <see cref="Jeebs.Internals.Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Action to run if <see cref="Jeebs.Internals.None{T}"/></param>
	/// <exception cref="UnknownMaybeException"></exception>
	public static void Switch<T>(Maybe<T> maybe, Action<T> some, Action<Msg> none)
	{
		// No return value so unable to use switch statement

		if (maybe is Some<T> x)
		{
			some(x.Value);
		}
		else if (maybe is None<T> y)
		{
			none(y.Reason);
		}
		else
		{
			throw new UnknownMaybeException(); // as Maybe<T> is internal implementation only this should never happen...
		}
	}

	/// <summary>
	/// Run a function depending on whether <paramref name="maybe"/> is a <see cref="Jeebs.Internals.Some{T}"/> or <see cref="Jeebs.Internals.None{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="U">Next value type</typeparam>
	/// <param name="maybe">Maybe being switched</param>
	/// <param name="some">Function to run if <see cref="Jeebs.Internals.Some{T}"/> - receives value <typeparamref name="T"/> as input</param>
	/// <param name="none">Function to run if <see cref="Jeebs.Internals.None{T}"/></param>
	/// <exception cref="UnknownMaybeException"></exception>
	public static U Switch<T, U>(Maybe<T> maybe, Func<T, U> some, Func<Msg, U> none) =>
		maybe switch
		{
			Some<T> x =>
				some(x.Value),

			None<T> x =>
				none(x.Reason),

			_ =>
				throw new UnknownMaybeException() // as Maybe<T> is internal implementation only this should never happen...
		};
}
