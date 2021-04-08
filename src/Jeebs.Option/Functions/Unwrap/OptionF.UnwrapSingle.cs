// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections;
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
					some: v => v switch
					{
						IList<U> list when list.Count == 1 =>
							Return(list.Single()),

						IList<U> list when list.Count == 0 =>
							None<U>(noItems?.Invoke() ?? new Msg.UnwrapSingleNoItemsMsg()),

						IList<U> =>
							None<U>(tooMany?.Invoke() ?? new Msg.UnwrapSingleTooManyItemsErrorMsg()),

						IList =>
							None<U, Msg.UnwrapSingleIncorrectTypeErrorMsg>(),

						_ =>
							None<U>(notAList?.Invoke() ?? new Msg.UnwrapSingleNotAListMsg())
					},
					none: r => new None<U>(r)
				),
				DefaultHandler
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Base UnwrapSingle error message</summary>
			public abstract record UnwrapSingleErrorMsg(UnwrapSingleError Error) : IMsg { }

			/// <summary>No items in the list</summary>
			public sealed record UnwrapSingleNoItemsMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.NoItems) { }

			/// <summary>Too many items in the list</summary>
			public sealed record UnwrapSingleTooManyItemsErrorMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.TooManyItems) { }

			/// <summary>Too many items in the list</summary>
			public sealed record UnwrapSingleIncorrectTypeErrorMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.IncorrectType) { }

			/// <summary>Not a list</summary>
			public sealed record UnwrapSingleNotAListMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.NoItems) { }

			/// <summary>
			/// Possible reasons for
			/// <see cref="UnwrapSingle{T, U}(Option{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/> failing
			/// </summary>
			public enum UnwrapSingleError
			{
				/// <inheritdoc cref="UnwrapSingleNoItemsMsg"/>
				NoItems,

				/// <inheritdoc cref="UnwrapSingleTooManyItemsErrorMsg"/>
				TooManyItems,

				/// <inheritdoc cref="UnwrapSingleIncorrectTypeErrorMsg"/>
				IncorrectType,

				/// <inheritdoc cref="UnwrapSingleNotAListMsg"/>
				NotAList
			}
		}
	}
}
