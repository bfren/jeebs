using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		new public async Task<IR<TValue>> RunAsync(Func<Task> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { await f().ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		private async Task<IR<TValue>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => x.Catch(async () => { await f(x).ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		new public Task<IR<TValue>> RunAsync(Func<IOk, Task> f)
			=> PrivateRunAsync(f);

		public Task<IR<TValue>> RunAsync(Func<IOk<TValue>, Task> f)
			=> PrivateRunAsync(f);

		public Task<IR<TValue>> RunAsync(Func<IOkV<TValue>, Task> f)
			=> PrivateRunAsync(f);
	}
}
