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
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TNext}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOk{TNext, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(
			this Task<IR<TResult, TState>> @this,
			Func<Task<TNext>> t
		) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult, TState> s => Catch<TResult, TNext, TState>(s, async () => { var v = await t().ConfigureAwait(false); return s.OkV(v); }),
			{ } r => SkipAhead<TResult, TNext, TState>(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOk{TNext, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(
			this Task<IR<TResult, TState>> @this,
			Func<IOk<TResult, TState>, Task<IR<TNext, TState>>> t
		) => await @this.ConfigureAwait(false) switch
		{
			IOk<TResult, TState> s => Catch(s, () => t(s)),
			{ } r => SkipAhead<TResult, TNext, TState>(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOkV{TNext, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(
			this Task<IR<TResult, TState>> @this, Func<IOkV<TResult, TState>, Task<IR<TNext, TState>>> t
		) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult, TState> s => Catch(s, () => t(s)),
			{ } r => SkipAhead<TResult, TNext, TState>(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="OkV{TNext, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOk{TNext, TState}"/> result</param>
		public static Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(this IR<TResult, TState> @this, Func<Task<TNext>> t) => LinkMapAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOk{TNext, TState}"/> result</param>
		public static Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(this IR<TResult, TState> @this, Func<IOk<TResult, TState>, Task<IR<TNext, TState>>> t) => LinkMapAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="IR{TNext}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="IOkV{TNext}"/> result</param>
		public static Task<IR<TNext, TState>> LinkMapAsync<TResult, TNext, TState>(this IR<TResult, TState> @this, Func<IOkV<TResult, TState>, Task<IR<TNext, TState>>> t) => LinkMapAsync(Task.Run(() => @this), t);

		#endregion
	}
}
