// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Use <paramref name="bind"/> to convert the value of <paramref name="maybe"/> to type <typeparamref name="U"/>,
	/// if it is a <see cref="Jeebs.Internals.Some{T}"/>
	/// </summary>
	/// <typeparam name="T">Maybe value type</typeparam>
	/// <typeparam name="U">Next value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="bind">Binding function - will receive <see cref="Jeebs.Internals.Some{T}.Value"/> if this is a <see cref="Jeebs.Internals.Some{T}"/></param>
	public static Maybe<U> Bind<T, U>(Maybe<T> maybe, Func<T, Maybe<U>> bind) =>
		Catch(() =>
			Switch(
				maybe,
				some: v => bind(v),
				none: r => None<U>(r)
			),
			DefaultHandler
		);
}
