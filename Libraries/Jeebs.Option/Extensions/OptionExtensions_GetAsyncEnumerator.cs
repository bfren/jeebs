// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: Await
	/// </summary>
	public static partial class OptionExtensions
	{
		/// <inheritdoc cref="Option{T}.GetEnumerator"/>
		/// <param name="this">Option (awaitable)</param>
		public static async IAsyncEnumerator<T> GetAsyncEnumerator<T>(this Task<Option<T>> @this)
		{
			if (await @this is Some<T> some)
			{
				yield return some.Value;
			}
		}
	}
}
