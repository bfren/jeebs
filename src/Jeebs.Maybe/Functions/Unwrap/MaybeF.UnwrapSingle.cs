// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace F;

public static partial class MaybeF
{
	/// <summary>
	/// Unwrap the value of <paramref name="maybe"/> - if it is <see cref="Jeebs.Internals.Some{T}"/>
	/// and <typeparamref name="TList"/> implements <see cref="IEnumerable{T}"/>
	/// </summary>
	/// <typeparam name="TList">List value type</typeparam>
	/// <typeparam name="TSingle">Single value type</typeparam>
	/// <param name="maybe">Input Maybe</param>
	/// <param name="noItems">Function to run if the Maybe value is a list with no items</param>
	/// <param name="tooMany">Function to run if the Maybe value is a list with more than one item</param>
	/// <param name="notAList">Function to run if the Maybe value is not a list</param>
	public static Maybe<TSingle> UnwrapSingle<TList, TSingle>(Maybe<TList> maybe, Func<Msg>? noItems, Func<Msg>? tooMany, Func<Msg>? notAList) =>
		Catch(() =>
			Switch(
				maybe,
				some: v => v switch
				{
					IList<TSingle> list when list.Count == 1 =>
						Some(list.Single()),

					IList<TSingle> list when list.Count == 0 =>
						None<TSingle>(noItems?.Invoke() ?? new M.UnwrapSingleNoItemsMsg()),

					IList<TSingle> =>
						None<TSingle>(tooMany?.Invoke() ?? new M.UnwrapSingleTooManyItemsErrorMsg()),

					IList =>
						None<TSingle, M.UnwrapSingleIncorrectTypeErrorMsg>(),

					_ =>
						None<TSingle>(notAList?.Invoke() ?? new M.UnwrapSingleNotAListMsg())
				},
				none: r => None<TSingle>(r)
			),
			DefaultHandler
		);

	/// <summary>Messages</summary>
	public static partial class M
	{
		/// <summary>Base UnwrapSingle error message</summary>
		/// <param name="Error">UnwrapSingleError</param>
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
		/// <see cref="UnwrapSingle{T, U}(Maybe{T}, Func{Msg}?, Func{Msg}?, Func{Msg}?)"/> failing
		/// </summary>
		public enum UnwrapSingleError
		{
			/// <inheritdoc cref="UnwrapSingleNoItemsMsg"/>
			NoItems = 0,

			/// <inheritdoc cref="UnwrapSingleTooManyItemsErrorMsg"/>
			TooManyItems = 1,

			/// <inheritdoc cref="UnwrapSingleIncorrectTypeErrorMsg"/>
			IncorrectType = 2,

			/// <inheritdoc cref="UnwrapSingleNotAListMsg"/>
			NotAList = 3
		}
	}
}
