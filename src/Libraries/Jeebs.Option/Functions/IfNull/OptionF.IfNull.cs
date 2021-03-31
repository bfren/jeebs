// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// If <paramref name="option"/> is <see cref="Jeebs.None{T}"/> and the reason is <see cref="Msg.NullValueMsg"/>,
		/// or <paramref name="option"/> is <see cref="Some{T}"/> and <see cref="Some{T}.Value"/> is null,
		/// runs <paramref name="nullValue"/> - which gives you the opportunity to return a more useful 'Not Found' message
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="nullValue">Runs if a null value was found</param>
		public static Option<T> IfNull<T>(Option<T> option, Func<Option<T>> nullValue) =>
			Catch(() =>
				option switch
				{
					Some<T> x when x.Value is null =>
						nullValue(),

					None<T> x when x.Reason is Msg.NullValueMsg =>
						nullValue(),

					_ =>
						option
				},
				DefaultHandler
			);
	}
}
