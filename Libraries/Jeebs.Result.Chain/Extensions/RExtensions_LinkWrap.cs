using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: LinkWrap
	/// </summary>
	public static class RExtensions_LinkWrap
	{
		/// <summary>
		/// Execute the next link in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk"/> result</param>
		public static IR<TNext> LinkWrap<TNext>(this IR @this, Func<TNext> f)
			=> @this switch
			{
				IOk ok => ok.Catch(() => { var v = f(); return ok.OkV(v); }),
				_ => @this.Error<TNext>()
			};

		/// <summary>
		/// Execute the next link in the chain and wrap in a new result type
		/// <para>Func <paramref name="f"/> receives no input and returns a value of type <typeparamref name="TNext"/>, which is then wrapped in an <see cref="IOkV{TValue, TState}"/> object</para>
		/// <para>Any exceptions will be caught and passed down the chain as a <see cref="Jm.ExceptionMsg"/> message</para>
		/// </summary>
		/// <typeparam name="TValue">Result value type</typeparam>
		/// <typeparam name="TState">Chain state type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">The function will be executed if <paramref name="this"/> is an <see cref="IOk{TValue, TState}"/> result</param>
		public static IR<TNext, TState> LinkWrap<TValue, TState, TNext>(this IR<TValue, TState> @this, Func<TNext> f)
			=> @this switch
			{
				IOk<TValue, TState> ok => ok.Catch(() => { var v = f(); return ok.OkV(v); }),
				_ => @this.Error<TNext>()
			};
	}
}
