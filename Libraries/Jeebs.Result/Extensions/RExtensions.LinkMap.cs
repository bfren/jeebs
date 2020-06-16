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
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives no input and returns a value of type <typeparamref name="TResult"/>, which is then wrapped in an <see cref="OkV{TResult}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="Ok{T}"/> result</param>
		public static async Task<R<TResult>> LinkMapAsync<TSource, TResult>(this Task<R<TSource>> @this, Func<Task<TResult>> t) => await @this.ConfigureAwait(false) switch
		{
			Ok<TSource> s => Catch<TSource, TResult>(s, async () => { var v = await t().ConfigureAwait(false); return s.OkV(v); }),
			{ } r => SkipAhead<TSource, TResult>(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="R{TResult}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="Ok{T}"/> result</param>
		public static async Task<R<TResult>> LinkMapAsync<TSource, TResult>(this Task<R<TSource>> @this, Func<Ok<TSource>, Task<R<TResult>>> t) => await @this.ConfigureAwait(false) switch
		{
			Ok<TSource> s => Catch(s, () => t(s)),
			{ } r => SkipAhead<TSource, TResult>(r)
		};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="R{TResult}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result task (will be awaited before executing <paramref name="t"/>)</param>
		/// <param name="t">The task will be executed if the current object is an <see cref="OkV{T}"/> result</param>
		public static async Task<R<TResult>> LinkMapAsync<TSource, TResult>(this Task<R<TSource>> @this, Func<OkV<TSource>, Task<R<TResult>>> t) => await @this.ConfigureAwait(false) switch
		{
			OkV<TSource> s => Catch(s, () => t(s)),
			{ } r => SkipAhead<TSource, TResult>(r)
		};

		#endregion

		#region Start Sync

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives no input and returns a value of type <typeparamref name="TResult"/>, which is then wrapped in an <see cref="OkV{TResult}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="Ok{T}"/> result</param>
		public static Task<R<TResult>> LinkMapAsync<TSource, TResult>(this R<TSource> @this, Func<Task<TResult>> t) => LinkMapAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="R{TResult}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="Ok{T}"/> result</param>
		public static Task<R<TResult>> LinkMapAsync<TSource, TResult>(this R<TSource> @this, Func<Ok<TSource>, Task<R<TResult>>> t) => LinkMapAsync(Task.Run(() => @this), t);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="t"/> receives the current object as an input and returns a new result of type <see cref="R{TResult}"/></para>
		/// <para>Any exceptions will be caught and passed down the pipeline as a <see cref="Jm.AsyncException"/> message</para>
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="this">Current result>/param>
		/// <param name="t">The task will be executed if the current object is an <see cref="OkV{T}"/> result</param>
		public static Task<R<TResult>> LinkMapAsync<TSource, TResult>(this R<TSource> @this, Func<OkV<TSource>, Task<R<TResult>>> t) => LinkMapAsync(Task.Run(() => @this), t);

		#endregion
	}
}
