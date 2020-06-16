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
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="Ok{TSource}"/> result</param>
		public static async Task<R<TSource>> LinkAsync<TSource>(this Task<R<TSource>> @this, Func<Task> t) => await @this.ConfigureAwait(false) switch
		{
			Ok<TSource> s => Catch<TSource, TSource>(s, async () => { await t().ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="Ok{TSource}"/> result</param>
		public static async Task<R<TSource>> LinkAsync<TSource>(this Task<R<TSource>> @this, Func<Ok<TSource>, Task> t) => await @this switch
		{
			Ok<TSource> s => Catch<TSource, TSource>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="OkV{TSource}"/> result</param>
		public static async Task<R<TSource>> LinkAsync<TSource>(this Task<R<TSource>> @this, Func<OkV<TSource>, Task> t) => await @this switch
		{
			OkV<TSource> s => Catch<TSource, TSource>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="R{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="Ok{TSource}"/> result</param>
		public static Task<R<TSource>> LinkAsync<TSource>(this R<TSource> @this, Func<Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="Ok{TSource}"/> result</param>
		public static Task<R<TSource>> LinkAsync<TSource>(this R<TSource> @this, Func<Ok<TSource>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TSource}.Ok()"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="OkV{TSource}"/> result</param>
		public static Task<R<TSource>> LinkAsync<TSource>(this R<TSource> @this, Func<OkV<TSource>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		#endregion
	}
}
