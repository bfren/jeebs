// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Msg = JeebsF.OptionFMsg;

namespace JeebsF
{
	public static partial class OptionF
	{
		/// <summary>
		/// Catch any unhandled exceptions in the chain
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="chain">The chain to execute</param>
		/// <param name="handler">[Optional] Unhandled exception handler</param>
		public static Option<T> Catch<T>(Func<Option<T>> chain, Handler? handler = null)
		{
			try
			{
				return chain();
			}
			catch (Exception e) when (handler != null)
			{
				return None<T>(handler(e));
			}
			catch (Exception e)
			{
				return None<T>(new Msg.UnhandledExceptionMsg(e));
			}
		}

		/// <summary>
		/// Catch any unhandled exceptions in the asynchronous chain
		/// </summary>
		/// <typeparam name="T">Option value type</typeparam>
		/// <param name="chain">The asynchronous chain to execute</param>
		/// <param name="handler">[Optional] Unhandled exception handler</param>
		public static async Task<Option<T>> CatchAsync<T>(Func<Task<Option<T>>> chain, Handler? handler = null)
		{
			try
			{
				return await chain();
			}
			catch (Exception e) when (handler != null)
			{
				return None<T>(handler(e));
			}
			catch (Exception e)
			{
				return None<T>(new Msg.UnhandledExceptionMsg(e));
			}
		}
	}
}

namespace JeebsF.OptionFMsg
{
	/// <summary>
	/// Unhandled exception
	/// See <see cref="OptionF.Catch{T}(Func{Option{T}}, OptionF.Handler?)"/> and
	/// <see cref="OptionF.CatchAsync{T}(Func{Task{Option{T}}}, OptionF.Handler?)"/>
	/// </summary>
	public sealed record UnhandledExceptionMsg(Exception Exception) : IExceptionMsg { }
}
