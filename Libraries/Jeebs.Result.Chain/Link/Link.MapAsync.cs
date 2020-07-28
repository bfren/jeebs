using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		/// <inheritdoc/>
		public async Task<IR<TNext>> MapAsync<TNext>(Func<IOk, Task<IR<TNext>>> f)
			=> result switch
			{
				IOk x => x.Catch(async () => await f(x).ConfigureAwait(false)),
				_ => result.Error<TNext>()
			};
	}
}
