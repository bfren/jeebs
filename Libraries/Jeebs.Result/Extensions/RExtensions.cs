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
		/// Skip ahead by returning the current object as an Error
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <param name="r">Source result</param>
		private static Error<TSource> SkipAhead<TSource>(R<TSource> r) => (Error<TSource>)r;

		/// <summary>
		/// Skip ahead by returning the current object as an Error
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="r">Source result</param>
		private static Error<TResult> SkipAhead<TSource, TResult>(R<TSource> r) => new Error<TResult> { Messages = r.Messages };

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TSource">Source value type</typeparam>
		/// <typeparam name="TResult">Result value type</typeparam>
		/// <param name="r">Source result</param>
		/// <param name="f">Async function to execute</param>
		private static R<TResult> Catch<TSource, TResult>(R<TSource> r, Func<Task<R<TResult>>> f)
		{
			try
			{
				return f().GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				r.Messages.Add(new Jm.AsyncException(ex));
				return SkipAhead<TSource, TResult>(r);
			}
		}
	}
}
