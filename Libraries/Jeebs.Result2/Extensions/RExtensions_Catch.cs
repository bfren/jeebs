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
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">Function to execute</param>
		internal static TResult Catch<TResult>(this IOk ok, Func<TResult> f)
			where TResult : IR
		{
			try
			{
				return f();
			}
			catch (Exception ex)
			{
				return (TResult)ok.Error().With().Messages(new Jm.ExceptionMsg(ex));
			}
		}

		/// <summary>
		/// Execute a function, catching any exceptions and skipping head with an error message
		/// </summary>
		/// <typeparam name="TNext">Next Result value type</typeparam>
		/// <param name="f">Function to execute</param>
		internal static TResult Catch<TResult>(this IOk ok, Func<Task<TResult>> f)
			where TResult : IR
		{
			try
			{
				return f().GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				return (TResult)ok.Error().With().Messages(new Jm.ExceptionMsg(ex));
			}
		}
	}
}
