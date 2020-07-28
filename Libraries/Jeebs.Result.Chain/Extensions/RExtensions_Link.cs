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
		/// <para>Action <paramref name="a"/> receives no input and returns <paramref name="this"/> - if it's an <see cref="IOk"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="a">The action will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static IR Link<TResult>(this TResult @this, Action a)
			where TResult : IR => @this switch
			{
				IOk ok => ok.Catch(() => { a(); return ok; }),
				_ => @this.Error()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="a">The action will be executed if <paramref name="this"/> is an <see cref="IOk{TValue}"/> result</param>
		public static IR<TValue> Link<TResult, TValue>(this TResult @this, Action<IOk<TValue>> a)
			where TResult : IR<TValue> => @this switch
			{
				IOk<TValue> ok => ok.Catch(() => { a(ok); return ok; }),
				_ => @this.Error()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOk{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="a">The action will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TValue, TState> Link<TResult, TValue, TState>(this TResult @this, Action<IOk<TValue, TState>> a)
			where TResult : IR<TValue, TState> => @this switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => { a(ok); return ok; }),
				_ => @this.Error()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="a">The action will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue}"/> result</param>
		public static IR<TValue> Link<TResult, TValue>(this TResult @this, Action<IOkV<TValue>> a)
			where TResult : IR<TValue> => @this switch
			{
				IOkV<TValue> okV => okV.Catch(() => { a(okV); return okV; }),
				_ => @this.Error()
			};

		/// <summary>
		/// Execute the next link in the chain
		/// <para>Action <paramref name="a"/> receives <paramref name="this"/> as input and returns it - if it's an <see cref="IOkV{TValue, TState}"/></para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="a">The action will be executed if <paramref name="this"/> is an <see cref="IOkV{TValue, TState}"/> result</param>
		public static IR<TValue, TState> Link<TResult, TValue, TState>(this TResult @this, Action<IOkV<TValue, TState>> a)
			where TResult : IR<TValue, TState> => @this switch
			{
				IOkV<TValue, TState> okV => okV.Catch(() => { a(okV); return okV; }),
				_ => @this.Error()
			};
	}
}
