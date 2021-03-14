// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;

namespace F
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

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unhandled exception</summary>
			public sealed record UnhandledExceptionMsg(Exception Exception) : IExceptionMsg { }
		}
	}
}
