using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public async Task<IR<TValue, TState>> WrapAsync(Func<Task<TValue>> f)
			=> result switch
			{
				IOk<TValue, TState> x => x.Catch(async () => { var v = await f().ConfigureAwait(false); return x.OkV(v); }),
				_ => result.Error()
			};
	}
}
