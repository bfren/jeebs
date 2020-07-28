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
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static IR<TNext> LinkMap<TNext>(this IR @this, Func<IOk, IR<TNext>> f)
			=> @this switch
			{
				IOk ok => ok.Catch(() => f(ok)),
				_ => @this.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static IR<TNext> LinkMap<TValue, TNext>(this IR<TValue> @this, Func<IOk<TValue>, IR<TNext>> f)
			=> @this switch
			{
				IOk<TValue> ok => ok.Catch(() => f(ok)),
				_ => @this.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOk{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkMap<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<IOk<TValue, TState>, IR<TNext, TState>> f)
			=> @this switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => f(ok)),
				_ => @this.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue}"/> - and returns a new <see cref="IR{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static IR<TNext> LinkMap<TValue, TNext>(this IR<TValue> @this, Func<IOkV<TValue>, IR<TNext>> f)
			=> @this switch
			{
				IOkV<TValue> okV => okV.Catch(() => f(okV)),
				_ => @this.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and map to a new result type
		/// <para>Func <paramref name="f"/> receives <paramref name="this"/> as input - if it's an <see cref="IOkV{TValue, TState}"/> - and returns a new <see cref="IR{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkMap<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<IOkV<TValue, TState>, IR<TNext, TState>> f)
			=> @this switch
			{
				IOkV<TValue, TState> okV => okV.Catch(() => f(okV)),
				_ => @this.Error<TNext>()
			};
	}
}
