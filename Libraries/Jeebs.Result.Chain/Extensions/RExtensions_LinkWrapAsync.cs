using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: LinkWrapAsync
	/// </summary>
	public static class RExtensions_LinkWrapAsync
	{
		/// <summary>
		/// Execute the next link asynchronously in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR<TNext>> LinkWrapAsync<TNext>(this Task<IR> @this, Func<Task<TNext>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => { var v = await f().ConfigureAwait(false); return ok.OkV(v); }),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static Task<IR<TNext>> LinkWrapAsync<TNext>(this IR @this, Func<Task<TNext>> f)
			=> LinkWrapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link asynchronously in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkWrapAsync<TValue, TState, TNext>(this Task<IR<TValue, TState>> @this, Func<Task<TNext>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => { var v = await f().ConfigureAwait(false); return ok.OkV(v); }),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static Task<IR<TNext, TState>> LinkWrapAsync<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<Task<TNext>> f)
			=> LinkWrapAsync(Task.FromResult(@this), f);
	}
}
