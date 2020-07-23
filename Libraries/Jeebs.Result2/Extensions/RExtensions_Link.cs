using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: Link
	/// </summary>
	public static class RExtensions_Link
	{
		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives no input and returns <paramref name="result"/> - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="a">The action will be executed if <paramref name="result"/> is an <see cref="IOk"/> result</param>
		public static IR Link<TResult>(this TResult result, Action a)
			where TResult : IR => result switch
			{
				IOk ok => ok.Catch(() => { a(); return ok; }),
				_ => result.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives no input and returns <paramref name="result"/> - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR> LinkAsync<TResult>(this Task<TResult> result, Func<Task> f)
			where TResult : IR => await result.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => { await f().ConfigureAwait(false); return ok; }),
				{ } r => r.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOk{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="a">The action will be executed if <paramref name="result"/> is an <see cref="IOk{TValue}"/> result</param>
		public static IR<TValue> Link<TResult, TValue>(this TResult result, Action<IOk<TValue>> a)
			where TResult : IR<TValue> => result switch
			{
				IOk<TValue> ok => ok.Catch(() => { a(ok); return ok; }),
				_ => result.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOk{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue}"/> result</param>
		public static async Task<IR<TValue>> LinkAsync<TResult, TValue>(this Task<TResult> result, Func<IOk<TValue>, Task> f)
			where TResult : IR<TValue> => await result.ConfigureAwait(false) switch
			{
				IOk<TValue> ok => ok.Catch(async () => { await f(ok).ConfigureAwait(false); return ok; }),
				{ } r => r.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="a">The action will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TValue, TState> Link<TResult, TValue, TState>(this TResult result, Action<IOk<TValue, TState>> a)
			where TResult : IR<TValue, TState> => result switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => { a(ok); return ok; }),
				_ => result.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TValue, TState>> LinkAsync<TResult, TValue, TState>(this Task<TResult> result, Func<IOk<TValue, TState>, Task> f)
			where TResult : IR<TValue, TState> => await result.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => { await f(ok).ConfigureAwait(false); return ok; }),
				{ } r => r.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOkV{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="a">The action will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static IR<TValue> Link<TResult, TValue>(this TResult result, Action<IOkV<TValue>> a)
			where TResult : IR<TValue> => result switch
			{
				IOkV<TValue> okV => okV.Catch(() => { a(okV); return okV; }),
				_ => result.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOkV{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static async Task<IR<TValue>> LinkAsync<TResult, TValue>(this Task<TResult> result, Func<IOkV<TValue>, Task> f)
			where TResult : IR<TValue> => await result.ConfigureAwait(false) switch
			{
				IOkV<TValue> okV => okV.Catch(async () => { await f(okV).ConfigureAwait(false); return okV; }),
				{ } r => r.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="a">The action will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static IR<TValue, TState> Link<TResult, TValue, TState>(this TResult result, Action<IOkV<TValue, TState>> a)
			where TResult : IR<TValue, TState> => result switch
			{
				IOkV<TValue, TState> okV => okV.Catch(() => { a(okV); return okV; }),
				_ => result.SkipAhead()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input and returns it - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static async Task<IR<TValue, TState>> LinkAsync<TResult, TValue, TState>(this Task<TResult> result, Func<IOkV<TValue, TState>, Task> f)
			where TResult : IR<TValue, TState> => await result.ConfigureAwait(false) switch
			{
				IOkV<TValue, TState> okV => okV.Catch(async () => { await f(okV).ConfigureAwait(false); return okV; }),
				{ } r => r.SkipAhead()
			};
	}
}
