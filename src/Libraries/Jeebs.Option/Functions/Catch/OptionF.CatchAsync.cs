// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
