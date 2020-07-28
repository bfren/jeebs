using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		public async Task<IR<TValue>> WrapAsync<TValue>(Func<Task<TValue>> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { var v = await f().ConfigureAwait(false); return x.OkV(v); }),
				_ => result.Error<TValue>()
			};
	}
}
