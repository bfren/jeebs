using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private async Task<IR<TNext>> PrivateMapAsync<TResult, TNext>(Func<TResult, Task<IR<TNext>>> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch(async () => await f(x).ConfigureAwait(false)),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc/>
		public Task<IR<TNext>> MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f0) =>
			PrivateMapAsync(f0);

		/// <inheritdoc/>
		public Task<IR<TNext>> MapAsync<TNext>(Func<IOk<TValue>, Task<IR<TNext>>> f1) =>
			PrivateMapAsync(f1);

		/// <inheritdoc/>
		public Task<IR<TNext>> MapAsync<TNext>(Func<IOkV<TValue>, Task<IR<TNext>>> f2) =>
			PrivateMapAsync(f2);
	}
}
