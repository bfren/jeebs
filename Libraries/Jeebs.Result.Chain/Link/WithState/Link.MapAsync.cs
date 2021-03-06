// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private async Task<IR<TNext, TState>> PrivateMapAsync<TResult, TNext>(Func<TResult, Task<IR<TNext, TState>>> f)
			where TResult : IOk<TValue, TState> =>
			result switch
			{
				TResult x =>
					Catch(async () => await f(x).ConfigureAwait(false)),

				_ =>
					result.Error<TNext>()
			};

		private async Task<IR<TNext, TState>> PrivateMapAsyncAddState<TResult, TNext>(Func<TResult, Task<IR<TNext>>> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch<TNext>(async () =>
						await f(x).ConfigureAwait(false) switch
						{
							IOk<TNext> y =>
								y.WithState(result.State),

							_ =>
								result.Error<TNext>()
						}
					),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc/>
		public Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f3) =>
			PrivateMapAsync(f3);

		/// <inheritdoc/>
		public Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f4) =>
			PrivateMapAsync(f4);

		#region Explicit implementations

		Task<IR<TNext, TState>> ILink<TValue, TState>.MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f0) =>
			PrivateMapAsyncAddState(f0);

		Task<IR<TNext, TState>> ILink<TValue, TState>.MapAsync<TNext>(Func<IOk<TValue>, Task<IR<TNext>>> f1) =>
			PrivateMapAsyncAddState(f1);

		Task<IR<TNext, TState>> ILink<TValue, TState>.MapAsync<TNext>(Func<IOkV<TValue>, Task<IR<TNext>>> f2) =>
			PrivateMapAsyncAddState(f2);

		#endregion
	}
}
