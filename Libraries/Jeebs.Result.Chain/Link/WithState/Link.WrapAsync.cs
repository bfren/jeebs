using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public async Task<IR<TNext, TState>> WrapAsync<TNext>(Func<Task<TNext>> f)
			=> result switch
			{
				IOk<TValue, TState> x => Catch<TNext>(async () => { var v = await f().ConfigureAwait(false); return x.OkV(v); }),
				_ => result.Error<TNext>()
			};
	}
}
