﻿// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Internals;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// If <paramref name="option"/> is <see cref="Jeebs.Internals.None{T}"/> and the reason is <see cref="M.NullValueMsg"/>,
	/// or <paramref name="option"/> is <see cref="Jeebs.Internals.Some{T}"/> and <see cref="Some{T}.Value"/> is null,
	/// runs <paramref name="ifNull"/> - which gives you the opportunity to return a more useful 'Not Found' message
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <param name="option">Input option</param>
	/// <param name="ifNull">Runs if a null value was found</param>
	public static Option<T> IfNull<T>(Option<T> option, Func<Option<T>> ifNull) =>
		Catch(() =>
			option switch
			{
				Some<T> x when x.Value is null =>
					ifNull(),

				None<T> x when x.Reason is M.NullValueMsg =>
					ifNull(),

				_ =>
					option
			},
			DefaultHandler
		);

	/// <inheritdoc cref="IfNull{T}(Option{T}, Func{Option{T}})"/>
	/// <typeparam name="T">Option value type</typeparam>
	/// <typeparam name="TMsg">Reason type</typeparam>
	public static Option<T> IfNull<T, TMsg>(Option<T> option, Func<TMsg> ifNull)
		where TMsg : Msg =>
		IfNull(option, () => None<T>(ifNull()));
}
