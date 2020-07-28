using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: LinkMapAsync
	/// </summary>
	public static class RExtensions_LinkMapAsync
	{
		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TNext>(this Task<IR> @this, Func<Task<IR<TNext>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => await f().ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static Task<IR<TNext>> LinkMapAsync<TNext>(this IR @this, Func<Task<IR<TNext>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TNext>(this Task<IR> @this, Func<IOk, Task<IR<TNext>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => await f(ok).ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static Task<IR<TNext>> LinkMapAsync<TNext>(this IR @this, Func<IOk, Task<IR<TNext>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TValue, TNext>(this Task<IR<TValue>> @this, Func<IOk<TValue>, Task<IR<TNext>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk<TValue> ok => ok.Catch(async () => await f(ok).ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static Task<IR<TNext>> LinkMapAsync<TValue, TNext>(this IR<TValue> @this, Func<IOk<TValue>, Task<IR<TNext>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TValue, TState, TNext>(this Task<IR<TValue, TState>> @this, Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => await f(ok).ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static Task<IR<TNext, TState>> LinkMapAsync<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TValue, TNext>(this Task<IR<TValue>> @this, Func<IOkV<TValue>, Task<IR<TNext>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOkV<TValue> okV => okV.Catch(async () => await f(okV).ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static Task<IR<TNext>> LinkMapAsync<TValue, TNext>(this IR<TValue> @this, Func<IOkV<TValue>, Task<IR<TNext>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TValue, TState, TNext>(this Task<IR<TValue, TState>> @this, Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOkV<TValue, TState> okV => okV.Catch(async () => await f(okV).ConfigureAwait(false)),
				{ } r => r.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static Task<IR<TNext, TState>> LinkMapAsync<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f)
			=> LinkMapAsync(Task.FromResult(@this), f);
	}
}
