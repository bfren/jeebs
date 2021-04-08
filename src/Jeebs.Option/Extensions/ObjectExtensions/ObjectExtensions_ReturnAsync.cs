// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	/// <summary>
	/// Object Extensions: ReturnAsync
	/// </summary>
	public static class ObjectExtensions_ReturnAsync
	{
		/// <inheritdoc cref="F.OptionF.ReturnAsync{T}(Func{Task{T}}, Handler)"/>
		public static Task<Option<T>> ReturnAsync<T>(this Func<Task<T>> @this, Handler handler) =>
			F.OptionF.ReturnAsync(@this, handler);

		/// <inheritdoc cref="F.OptionF.ReturnAsync{T}(Func{Task{T}}, bool, Handler)"/>
		public static Task<Option<T?>> ReturnAsync<T>(this Func<Task<T?>> @this, bool allowNull, Handler handler) =>
			F.OptionF.ReturnAsync(@this, allowNull, handler);
	}
}
