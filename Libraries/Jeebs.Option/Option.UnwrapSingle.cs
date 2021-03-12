// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jeebs;

namespace JeebsF
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Unwrap the single value of this option - if this is a <see cref="Some{T}"/>
		/// and <typeparamref name="T"/> implements <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <typeparam name="U">Single value type</typeparam>
		/// <param name="noItems">[Optional] Function to run if the Option value is a list with no items</param>
		/// <param name="tooMany">[Optional] Function to run if the Option value is a list with more than one item</param>
		/// <param name="notAList">[Optional] Function to run if the Option value is not a list</param>
		internal Option<U> DoUnwrapSingle<U>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
			OptionF.Catch(() =>
				Switch(
					some: v =>
						v switch
						{
							IEnumerable<U> list when list.Count() == 1 =>
								OptionF.Return(list.Single()),

							IEnumerable<U> list when !list.Any() =>
								OptionF.None<U>(noItems?.Invoke() ?? new Jm.Option.UnwrapSingleNoItemsMsg()),

							IEnumerable<U> =>
								OptionF.None<U>(tooMany?.Invoke() ?? new Jm.Option.UnwrapSingleTooManyItemsErrorMsg()),

							_ =>
								OptionF.None<U>(notAList?.Invoke() ?? new Jm.Option.UnwrapSingleNotAListMsg())
						},

					none: r =>
						new None<U>(r)
				)
			);

		/// <inheritdoc cref="DoUnwrapSingle{U}(Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
		public Option<U> UnwrapSingle<U>(Func<IMsg>? noItems = null, Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
			DoUnwrapSingle<U>(noItems, tooMany, notAList);
	}
}
