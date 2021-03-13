// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Return the current type if it is <see cref="Some{T}"/> and the predicate is true
		/// </summary>
		/// <param name="predicate">Predicate - if this is a <see cref="Some{T}"/> it receives the Value</param>
		/// <param name="handler">[Optional] Exception handler</param>
		internal Task<Option<T>> DoFilterAsync(Func<T, Task<bool>> predicate, Handler? handler) =>
			BindAsync(
				async x =>
					await predicate(x) switch
					{
						true =>
							Return(x),

						false =>
							None<T, Msg.PredicateWasFalseMsg>()
					},
				handler
			);

		/// <inheritdoc cref="DoFilterAsync(Func{T, Task{bool}}, Handler?)"/>
		public Task<Option<T>> FilterAsync(Func<T, Task<bool>> predicate, Handler? handler = null) =>
			DoFilterAsync(predicate, handler);
	}
}
