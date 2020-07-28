using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		new public async Task<IR<TValue, TState>> RunAsync(Func<Task> f)
			=> result switch
			{
				IOk x => x.Catch(async () => { await f().ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		private async Task<IR<TValue, TState>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => x.Catch(async () => { await f(x).ConfigureAwait(false); return result; }),
				_ => result.Error()
			};

		new public Task<IR<TValue, TState>> RunAsync(Func<IOk, Task> f)
			=> PrivateRunAsync(f);

		new public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue>, Task> f)
			=> PrivateRunAsync(f);

		new public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue>, Task> f)
			=> PrivateRunAsync(f);

		public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue, TState>, Task> f)
			=> PrivateRunAsync(f);

		public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue, TState>, Task> f)
			=> PrivateRunAsync(f);
	}
}
