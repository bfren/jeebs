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
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IOk{T}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TSource}"/> result</param>
		public static async Task<IR<TSource>> LinkAsync<TSource>(this Task<IR<TSource>> @this, Func<Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TSource> s => Catch<TSource, TSource>(s, async () => { await t().ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IOk{T}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TSource}"/> result</param>
		public static async Task<IR<TSource>> LinkAsync<TSource>(this Task<IR<TSource>> @this, Func<IOk<TSource>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TSource> s => Catch<TSource, TSource>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TSource}"/> result</param>
		public static async Task<IR<TSource>> LinkAsync<TSource>(this Task<IR<TSource>> @this, Func<IOkV<TSource>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TSource> s => Catch<TSource, TSource>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IR{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TSource}"/> result</param>
		public static Task<IR<TSource>> LinkAsync<TSource>(this IR<TSource> @this, Func<Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TSource}"/> result</param>
		public static Task<IR<TSource>> LinkAsync<TSource>(this IR<TSource> @this, Func<IOk<TSource>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TSource}"/> result</param>
		public static Task<IR<TSource>> LinkAsync<TSource>(this IR<TSource> @this, Func<IOkV<TSource>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		#endregion
	}
}
