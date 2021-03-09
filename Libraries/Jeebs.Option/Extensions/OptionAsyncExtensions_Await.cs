// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Enable Option chaining and piping
	/// </summary>
	public static partial class OptionAsyncExtensions
	{
		/// <summary>
		/// Use sparingly - blocks the calling thread as it forces the task to run synchronously
		/// </summary>
		/// <typeparam name="T">Value type</typeparam>
		/// <param name="this">Option (awaitable)</param>
		public static Option<T> Await<T>(this Task<Option<T>> @this) =>
			@this.GetAwaiter().GetResult();
	}
}
