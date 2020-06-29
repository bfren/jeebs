using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions_WithState
	{
		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TResult}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TResult, TState> SkipAhead<TResult, TState>(IR<TResult, TState> r) => (IError<TResult, TState>)r;

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TNext, TState> SkipAhead<TResult, TNext, TState>(IR<TResult, TState> r) => new Error<TNext, TState>(r.State) { Messages = r.Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="r">Source result</param>
		/// <param name="f">Async function to execute</param>
		private static IR<TNext, TState> Catch<TResult, TNext, TState>(IR<TResult, TState> r, Func<Task<IR<TNext, TState>>> f)
		{
			try
			{
				return f().GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				r.Messages.Add(new Jm.AsyncException(ex));
				return SkipAhead<TResult, TNext, TState>(r);
			}
		}
	}
}
