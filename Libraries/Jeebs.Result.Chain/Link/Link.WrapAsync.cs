using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public async Task<IR<TNext>> WrapAsync<TNext>(Func<Task<TNext>> f)
			=> result switch
			{
				IOk x => Catch<TNext>(async () => { var v = await f().ConfigureAwait(false); return x.OkV(v); }),
				_ => result.Error<TNext>()
			};
	}
}
