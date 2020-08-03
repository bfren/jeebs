using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private async Task<IR<TNext, TState>> PrivateMapAsync<TResult, TNext>(Func<TResult, Task<IR<TNext, TState>>> f)
			where TResult : IOk<TValue, TState>
			=> result switch
			{
				TResult x => Catch(async () => await f(x)),
				_ => result.Error<TNext>()
			};

		/// <inheritdoc/>
		public Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> PrivateMapAsync(f);

		/// <inheritdoc/>
		public Task<IR<TNext, TState>> MapAsync<TNext>(Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> PrivateMapAsync(f);
	}
}
