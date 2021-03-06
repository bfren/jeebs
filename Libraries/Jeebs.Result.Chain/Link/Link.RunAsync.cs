// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private async Task<IR<TValue>> PrivateRunAsync<TResult>(Func<TResult, Task> f)
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
		public async Task<IR<TValue>> RunAsync(Func<Task> f0) =>
			result switch
			{
				IOk<TValue> x =>
					Catch(async () =>
					{
						await f0().ConfigureAwait(false);
						return result;
					}),

				_ =>
					result.Error()
			};

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOk, Task> f1) =>
			PrivateRunAsync(f1);

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOk<TValue>, Task> f2) =>
			PrivateRunAsync(f2);

		/// <inheritdoc/>
		public Task<IR<TValue>> RunAsync(Func<IOkV<TValue>, Task> f3) =>
			PrivateRunAsync(f3);
	}
}
