// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F;

public static partial class OptionF
{
	/// <summary>
	/// Unwrap the value of <paramref name="option"/> - if it is <see cref="Jeebs.Internals.Some{T}"/>
	/// and <typeparamref name="T"/> implements <see cref="IEnumerable{T}"/>
	/// </summary>
	/// <typeparam name="T">Option value type</typeparam>
	/// <typeparam name="U">Single value type</typeparam>
	/// <param name="option">Input option</param>
	/// <param name="noItems">Function to run if the Option value is a list with no items</param>
	/// <param name="tooMany">Function to run if the Option value is a list with more than one item</param>
	/// <param name="notAList">Function to run if the Option value is not a list</param>
	public static Option<U> UnwrapSingle<T, U>(Option<T> option, Func<Msg>? noItems, Func<Msg>? tooMany, Func<Msg>? notAList) =>
		Catch(() =>
			Switch(
				option,
				some: v => v switch
				{
					IList<U> list when list.Count == 1 =>
						Some(list.Single()),

					IList<U> list when list.Count == 0 =>
						None<U>(noItems?.Invoke() ?? new M.UnwrapSingleNoItemsMsg()),

					IList<U> =>
						None<U>(tooMany?.Invoke() ?? new M.UnwrapSingleTooManyItemsErrorMsg()),

					IList =>
						None<U, M.UnwrapSingleIncorrectTypeErrorMsg>(),

					_ =>
						None<U>(notAList?.Invoke() ?? new M.UnwrapSingleNotAListMsg())
				},
				none: r => None<U>(r)
			),
			DefaultHandler
		);

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Base UnwrapSingle error message</summary>
		public abstract record class UnwrapSingleErrorMsg(UnwrapSingleError Error) : Msg;

		/// <summary>No items in the list</summary>
		public sealed record class UnwrapSingleNoItemsMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.NoItems) { }

		/// <summary>Too many items in the list</summary>
		public sealed record class UnwrapSingleTooManyItemsErrorMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.TooManyItems) { }

		/// <summary>Too many items in the list</summary>
		public sealed record class UnwrapSingleIncorrectTypeErrorMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.IncorrectType) { }

		/// <summary>Not a list</summary>
		public sealed record class UnwrapSingleNotAListMsg() : UnwrapSingleErrorMsg(UnwrapSingleError.NoItems) { }

		/// <summary>
		/// Possible reasons for
		/// <see cref="UnwrapSingle{T, U}(Option{T}, Func{Msg}?, Func{Msg}?, Func{Msg}?)"/> failing
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
