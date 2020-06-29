using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions
	{
		#region Start Async

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IOk{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult}"/> result</param>
		public static async Task<IR<TResult>> LinkAsync<TResult>(this Task<IR<TResult>> @this, Func<Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult> s => Catch<TResult, TResult>(s, async () => { await t().ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IOk{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult}"/> result</param>
		public static async Task<IR<TResult>> LinkAsync<TResult>(this Task<IR<TResult>> @this, Func<IOk<TResult>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult> s => Catch<TResult, TResult>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TResult}"/> result</param>
		public static async Task<IR<TResult>> LinkAsync<TResult>(this Task<IR<TResult>> @this, Func<IOkV<TResult>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult> s => Catch<TResult, TResult>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IR{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult}"/> result</param>
		public static Task<IR<TResult>> LinkAsync<TResult>(this IR<TResult> @this, Func<Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult}"/> result</param>
		public static Task<IR<TResult>> LinkAsync<TResult>(this IR<TResult> @this, Func<IOk<TResult>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TResult}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TResult}"/> result</param>
		public static Task<IR<TResult>> LinkAsync<TResult>(this IR<TResult> @this, Func<IOkV<TResult>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		#endregion
	}
}
