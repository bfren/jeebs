using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private async Task<IR<TValue>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => Catch(async () => { await f(x).ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		public async Task<IR<TValue>> RunAsync(Func<Task> f)
			=> result switch
			{
				IOk<TValue> x => Catch(async () => { await f().ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOk, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOk<TValue>, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOkV<TValue>, Task> f)
			=> PrivateRunAsync(f);
	}
}
