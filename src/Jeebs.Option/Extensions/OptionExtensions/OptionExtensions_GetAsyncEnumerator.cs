// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Internals;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: GetAsyncEnumerator
	/// </summary>
	public static class OptionExtensions_GetAsyncEnumerator
	{
		/// <inheritdoc cref="Option{T}.GetEnumerator"/>
		public static async IAsyncEnumerator<T> GetAsyncEnumerator<T>(this Task<Option<T>> @this)
		{
			if (await @this is Some<T> some)
			{
				yield return some.Value;
			}
		}
	}
}
