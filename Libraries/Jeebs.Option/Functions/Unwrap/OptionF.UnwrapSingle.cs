// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <summary>
		/// Unwrap the value of <paramref name="option"/> - if it is <see cref="Some{T}"/>
		/// and <typeparamref name="T"/> implements <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <typeparam name="U">Single value type</typeparam>
		/// <param name="option">Input option</param>
		/// <param name="noItems">[Optional] Function to run if the Option value is a list with no items</param>
		/// <param name="tooMany">[Optional] Function to run if the Option value is a list with more than one item</param>
		/// <param name="notAList">[Optional] Function to run if the Option value is not a list</param>
		public static Option<U> UnwrapSingle<T, U>(Option<T> option, Func<IMsg>? noItems, Func<IMsg>? tooMany, Func<IMsg>? notAList) =>
			Catch(() =>
				Switch(
					option,
					some: v =>
						v switch
						{
							IEnumerable<U> list when list.Count() == 1 =>
								Return(list.Single()),

							IEnumerable<U> list when !list.Any() =>
								None<U>(noItems?.Invoke() ?? new Msg.UnwrapSingleNoItemsMsg()),

							IEnumerable<U> =>
								None<U>(tooMany?.Invoke() ?? new Msg.UnwrapSingleTooManyItemsErrorMsg()),

							_ =>
								None<U>(notAList?.Invoke() ?? new Msg.UnwrapSingleNotAListMsg())
						},

					none: r =>
						new None<U>(r)
				)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>No items in the list/// </summary>
			public sealed record UnwrapSingleNoItemsMsg : IMsg { }

			/// <summary>Too many items in the list</summary>
			public sealed record UnwrapSingleTooManyItemsErrorMsg : IMsg { }

			/// <summary>Not a list</summary>
			public sealed record UnwrapSingleNotAListMsg : IMsg { }
		}
	}
}
