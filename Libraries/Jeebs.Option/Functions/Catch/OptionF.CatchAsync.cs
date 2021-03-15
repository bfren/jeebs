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
		internal static async Task<Option<T>> CatchAsync<T>(Func<Task<Option<T>>> f, Handler handler)
		{
			try
			{
				return await f();
			}
			catch (Exception e)
			{
				return None<T>(handler(e));
			}
		}
	}
}
