using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public static partial class RExtensions
	{
		/// <summary>
		/// Add state to the current result object.
		/// </summary>
		/// <typeparam name="TState">State value type</typeparam>
		/// <param name="state">State value</param>
		public static async Task<IR<TResult, TState>> AddState<TResult, TState>(this Task<IR<TResult>> @this, TState state) => await @this.ConfigureAwait(false) switch
		{
			IOkV<TResult> okV => new OkV<TResult, TState>(okV.Val, state) { Messages = okV.Messages },
			IOk<TResult> ok => new Ok<TResult, TState>(state) { Messages = ok.Messages },
			IError error => new Error<TResult, TState>(state) { Messages = error.Messages },
			{ } r => throw new InvalidOperationException($"Unknown R<> subtype: '{r.GetType()}'.")
		};

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="IError{TResult}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TResult> SkipAhead<TResult>(IR<TResult> r) => (IError<TResult>)r;

		/// <summary>
		/// Skip ahead by returning the current object as an <see cref="Error{TNext}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="r">Source result</param>
		private static IError<TNext> SkipAhead<TResult, TNext>(IR<TResult> r) => new Error<TNext> { Messages = r.Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message, returning an <see cref="IR{TNext}"/>
		/// </summary>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <typeparam name="TNext">Next result value type</typeparam>
		/// <param name="r">Source result</param>
		/// <param name="f">Async function to execute</param>
		private static IR<TNext> Catch<TResult, TNext>(IR<TResult> r, Func<Task<IR<TNext>>> f)
		{
			try
			{
				return f().GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				r.Messages.Add(new Jm.AsyncException(ex));
				return SkipAhead<TResult, TNext>(r);
			}
		}
	}
}
