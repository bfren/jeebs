using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: LinkAsync
	/// </summary>
	public static class RExtensions_LinkAsync
	{
		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives no input and returns <paramref name="this"/> - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR> LinkAsync(this Task<IR> @this, Func<Task> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => { await f().ConfigureAwait(false); return ok; }),
				{ } r => r.Error()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives no input and returns <paramref name="this"/> - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static Task<IR> LinkAsync(this IR @this, Func<Task> f)
			=> LinkAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static async Task<IR<TValue>> LinkAsync<TValue>(this Task<IR<TValue>> @this, Func<IOk<TValue>, Task> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk<TValue> ok => ok.Catch(async () => { await f(ok).ConfigureAwait(false); return ok; }),
				{ } r => r.Error()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static Task<IR<TValue>> LinkAsync<TValue>(this IR<TValue> @this, Func<IOk<TValue>, Task> f)
			=> LinkAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TValue, TState>> LinkAsync<TValue, TState>(this Task<IR<TValue, TState>> @this, Func<IOk<TValue, TState>, Task> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => { await f(ok).ConfigureAwait(false); return ok; }),
				{ } r => r.Error()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static Task<IR<TValue, TState>> LinkAsync<TValue, TState>(this IR<TValue, TState> @this, Func<IOk<TValue, TState>, Task> f)
			=> LinkAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static async Task<IR<TValue>> LinkAsync<TValue>(this Task<IR<TValue>> @this, Func<IOkV<TValue>, Task> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOkV<TValue> okV => okV.Catch(async () => { await f(okV).ConfigureAwait(false); return okV; }),
				{ } r => r.Error()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static Task<IR<TValue>> LinkAsync<TValue>(this IR<TValue> @this, Func<IOkV<TValue>, Task> f)
			=> LinkAsync(Task.FromResult(@this), f);

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static async Task<IR<TValue, TState>> LinkAsync<TValue, TState>(this Task<IR<TValue, TState>> @this, Func<IOkV<TValue, TState>, Task> f)
			=> await @this.ConfigureAwait(false) switch
			{
				IOkV<TValue, TState> okV => okV.Catch(async () => { await f(okV).ConfigureAwait(false); return okV; }),
				{ } r => r.Error()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static Task<IR<TValue, TState>> LinkAsync<TValue, TState>(this IR<TValue, TState> @this, Func<IOkV<TValue, TState>, Task> f)
			=> LinkAsync(Task.FromResult(@this), f);
	}
}
