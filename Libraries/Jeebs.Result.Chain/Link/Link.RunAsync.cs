using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		/// <inheritdoc/>
		public async Task<IR> RunAsync(Func<Task> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { await f().ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		public async Task<IR> RunAsync(Func<IOk, Task> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { await f(x).ConfigureAwait(false); return result; }),
				_ => result.Error()
			};
	}
}
