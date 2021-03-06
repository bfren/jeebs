// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private async Task<IR<TValue, TState>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch(async () =>
					{
						await f(x).ConfigureAwait(false);
						return result;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		new public async Task<IR<TValue, TState>> RunAsync(Func<Task> f0) =>
			result switch
			{
				IOk x =>
					Catch(async () =>
					{
						await f0().ConfigureAwait(false);
						return result;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOk, Task> f1) =>
			PrivateRunAsync(f1);

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue>, Task> f2) =>
			PrivateRunAsync(f2);

		/// <inheritdoc/>
		new public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue>, Task> f3) =>
			PrivateRunAsync(f3);

		/// <inheritdoc/>
		public Task<IR<TValue, TState>> RunAsync(Func<IOk<TValue, TState>, Task> f4) =>
			PrivateRunAsync(f4);

		/// <inheritdoc/>
		public Task<IR<TValue, TState>> RunAsync(Func<IOkV<TValue, TState>, Task> f5) =>
			PrivateRunAsync(f5);
	}
}
