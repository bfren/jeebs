using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private async Task<IR<TValue, TState>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => x.Catch(async () => { await f(x).ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		new public async Task<IR<TValue, TState>> RunAsync(Func<Task> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { await f().ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOk, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue>, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue>, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue, TState>, Task> f)
			=> PrivateRunAsync(f);

		/// <inheritdoc/>
		public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue, TState>, Task> f)
			=> PrivateRunAsync(f);
	}
}
