// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public async Task<IR<TNext, TState>> WrapAsync<TNext>(Func<Task<TNext>> f) =>
			result switch
			{
				IOk<TValue, TState> x =>
					Catch<TNext>(async () => x.OkV(await f().ConfigureAwait(false))),

				_ =>
					result.Error<TNext>()
			};
	}
}
