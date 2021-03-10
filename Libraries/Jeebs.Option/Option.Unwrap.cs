// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using Jm.Option;

namespace Jeebs
{
	public abstract partial record Option<T>
	{
		/// <summary>
		/// Unwrap the value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <param name="ifNone">Function to return <typeparamref name="T"/> if this is a <see cref="None{T}"/></param>
		public T Unwrap(Func<T> ifNone) =>
			Switch(
				some: x => x,
				none: ifNone
			);

		/// <summary>
		/// Unwrap the single value of this option - if this is a <see cref="Some{T}"/>
		/// </summary>
		/// <typeparam name="U">Single value type</typeparam>
		public Option<U> UnwrapSingle<U>(Func<IMsg>? tooMany = null, Func<IMsg>? notAList = null) =>
			Option.Catch(() =>
				Switch(
					some: x =>
						x switch
						{
							IEnumerable<U> list when list.Count() == 1 =>
								Option.Wrap(list.Single()),

							IEnumerable<U> =>
								Option.None<U>(tooMany?.Invoke() ?? new UnwrapSingleTooManyItemsErrorMsg()),

							_ =>
								Option.None<U>(notAList?.Invoke() ?? new UnwrapSingleNotAListMsg())
						},

					none: r =>
						Option.None<U>(r)
				)
			);
	}
}
