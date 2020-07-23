using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: LinkMap
	/// </summary>
	public static class RExtensions_LinkMap
	{
		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk"/> result</param>
		public static IR<TNext> LinkMap<TResult, TNext>(this TResult result, Func<TNext> f)
			where TResult : IR => result switch
			{
				IOk ok => ok.Catch(() => { var v = f(); return ok.OkV(v); }),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TResult, TNext>(this Task<TResult> result, Func<Task<TNext>> f)
			where TResult : IR => await result.ConfigureAwait(false) switch
			{
				IOk ok => ok.Catch(async () => { var v = await f().ConfigureAwait(false); return ok.OkV(v); }),
				{ } r => r.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkMap<TResult, TValue, TState, TNext>(this TResult result, Func<TNext> f)
			where TResult : IR<TValue, TState> => result switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => { var v = f(); return ok.OkV(v); }),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TValue, TState, TNext>(this Task<TResult> result, Func<Task<TNext>> f)
			where TResult : IR<TValue, TState> => await result.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => { var v = await f().ConfigureAwait(false); return ok.OkV(v); }),
				{ } r => r.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOk{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue}"/> result</param>
		public static IR<TNext> LinkMap<TResult, TValue, TNext>(this TResult result, Func<IOk<TValue>, IR<TNext>> f)
			where TResult : IR<TValue> => result switch
			{
				IOk<TValue> ok => ok.Catch(() => f(ok)),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link asynchronously in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOk{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue}"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TResult, TValue, TNext>(this Task<TResult> result, Func<IOk<TValue>, Task<IR<TNext>>> f)
			where TResult : IR<TValue> => await result.ConfigureAwait(false) switch
			{
				IOk<TValue> ok => ok.Catch(async () => await f(ok).ConfigureAwait(false)),
				{ } r => r.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOk{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkMap<TResult, TValue, TState, TNext>(this TResult result, Func<IOk<TValue, TState>, IR<TNext, TState>> f)
			where TResult : IR<TValue, TState> => result switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => f(ok)),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOk{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TValue, TState, TNext>(this Task<TResult> result, Func<IOk<TValue, TState>, Task<IR<TNext, TState>>> f)
			where TResult : IR<TValue, TState> => await result.ConfigureAwait(false) switch
			{
				IOk<TValue, TState> ok => ok.Catch(async () => await f(ok).ConfigureAwait(false)),
				{ } r => r.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOkV{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static IR<TNext> LinkMap<TResult, TValue, TNext>(this TResult result, Func<IOkV<TValue>, IR<TNext>> f)
			where TResult : IR<TValue> => result switch
			{
				IOkV<TValue> okV => okV.Catch(() => f(okV)),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOkV{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static async Task<IR<TNext>> LinkMapAsync<TResult, TValue, TNext>(this Task<TResult> result, Func<IOkV<TValue>, Task<IR<TNext>>> f)
			where TResult : IR<TValue> => await result.ConfigureAwait(false) switch
			{
				IOkV<TValue> okV => okV.Catch(async () => await f(okV).ConfigureAwait(false)),
				{ } r => r.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOkV{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result</param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkMap<TResult, TValue, TState, TNext>(this TResult result, Func<IOkV<TValue, TState>, IR<TNext, TState>> f)
			where TResult : IR<TValue, TState> => result switch
			{
				IOkV<TValue, TState> okV => okV.Catch(() => f(okV)),
				_ => result.SkipAhead<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain asynchronously and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="result"/> as input - if it's an <see cref="IOkV{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="result">Result task - will be awaited before executing <paramref name="f"/></param>
		/// <param name="f">The function will be executed if <paramref name="result"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static async Task<IR<TNext, TState>> LinkMapAsync<TResult, TValue, TState, TNext>(this Task<TResult> result, Func<IOkV<TValue, TState>, Task<IR<TNext, TState>>> f)
			where TResult : IR<TValue, TState> => await result.ConfigureAwait(false) switch
			{
				IOkV<TValue, TState> okV => okV.Catch(async () => await f(okV).ConfigureAwait(false)),
				{ } r => r.SkipAhead<TNext>()
			};
	}
}
