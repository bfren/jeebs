// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// <see cref="Option{T}"/> Extensions: IfNullAsync
	/// </summary>
	public static class OptionExtensions_IfNullAsync
	{
		/// <inheritdoc cref="F.OptionF.IfNullAsync{T}(Option{T}, Func{Task{Option{T}}}))"/>
		public static Task<Option<T>> IfNullAsync<T>(this Task<Option<T>> @this, Func<Option<T>> nullValue) =>
			F.OptionF.IfNullAsync(@this, () => Task.FromResult(nullValue()));

		/// <inheritdoc cref="F.OptionF.IfNullAsync{T}(Option{T}, Func{Task{Option{T}}}))"/>
		public static Task<Option<T>> IfNullAsync<T>(this Task<Option<T>> @this, Func<Task<Option<T>>> nullValue) =>
			F.OptionF.IfNullAsync(@this, nullValue);
	}
}
