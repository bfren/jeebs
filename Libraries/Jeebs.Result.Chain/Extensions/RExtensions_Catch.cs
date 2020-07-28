using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Extension methods for IR interface: Catch
	/// </summary>
	internal static class RExtensions_Catch
	{
		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">Function to execute</param>
		internal static TResult Catch<TResult>(this IOk @this, Func<TResult> f)
			where TResult : IR
		{
			try
			{
				return f();
			}
			catch (Exception ex)
			{
				return (TResult)@this.Error().AddMsg(new Jm.ChainExceptionMsg(ex));
			}
		}

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TResult">Result type</typeparam>
		/// <param name="this">Result</param>
		/// <param name="f">Function to execute</param>
		internal static TResult Catch<TResult>(this IOk @this, Func<Task<TResult>> f)
			where TResult : IR
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				return (TResult)@this.Error().AddMsg(new Jm.ChainExceptionMsg(ex));
			}
		}
	}
}
