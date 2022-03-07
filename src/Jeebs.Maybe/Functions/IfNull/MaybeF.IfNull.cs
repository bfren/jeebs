// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// If <paramref name="maybe"/> is <see cref="Jeebs.Internals.None{T}"/> and the reason is <see cref="M.NullValueMsg"/>,
	/// or <paramref name="maybe"/> is <see cref="Jeebs.Internals.Some{T}"/> and <see cref="Some{T}.Value"/> is null,
	/// runs <paramref name="ifNull"/> - which gives you the opportunity to return a more useful 'Not Found' message
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="ifNull">Runs if a null value was found</param>
	public static Maybe<T> IfNull<T>(Maybe<T> maybe, Func<Maybe<T>> ifNull) =>
		Catch(() =>
			maybe switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				_ =>
					maybe
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Maybe{T}, Func{Maybe{T}})"/>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="TMsg">Reason type</typeparam>
	public static Maybe<T> IfNull<T, TMsg>(Maybe<T> maybe, Func<TMsg> ifNull)
		where TMsg : Msg =>
		IfNull(maybe, () => None<T>(ifNull()));
}
