using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions_WithState
	{
		#region Start Async

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IOk{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult, TState}"/> result</param>
		public static async Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this Task<IR<TResult, TState>> @this, Func<Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult, TState> s => Catch<TResult, TResult, TState>(s, async () => { await t().ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult, TState}"/> result</param>
		public static async Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this Task<IR<TResult, TState>> @this, Func<IOk<TResult, TState>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult, TState> s => Catch<TResult, TResult, TState>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TResult, TState}"/> result</param>
		public static async Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this Task<IR<TResult, TState>> @this, Func<IOkV<TResult, TState>, Task> t) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult, TState> s => Catch<TResult, TResult, TState>(s, async () => { await t(s).ConfigureAwait(false); return s.Ok(); }),
			{ } r => SkipAhead(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives no input and returns <see cref="IR{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult, TState}"/> result</param>
		public static Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this IR<TResult, TState> @this, Func<Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="R{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOk{TResult, TState}"/> result</param>
		public static Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this IR<TResult, TState> @this, Func<IOk<TResult, TState>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns <see cref="IR{TResult, TState}"/> as output</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.Exception"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result</param>
		/// <param name="t">The task will be executed if <paramref name="this"/> is an <see cref="IOkV{TResult, TState}"/> result</param>
		public static Task<IR<TResult, TState>> LinkAsync<TResult, TState>(this IR<TResult, TState> @this, Func<IOkV<TResult, TState>, Task> t) => LinkAsync(Task.Run(() => @this), t);

		#endregion
	}
}
