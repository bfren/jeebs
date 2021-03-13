// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;

namespace F
{
	public static partial class OptionF
	{
		/// <inheritdoc cref="Catch{T}(Func{Option{T}}, Handler?)"/>
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
