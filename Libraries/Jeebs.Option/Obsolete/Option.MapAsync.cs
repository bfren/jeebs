// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		[Obsolete]
		internal Task<Option<U>> DoMapAsync<U>(Func<T, Task<U>> map, Handler? handler) =>
			F.OptionF.MapAsync(this, map, handler);
	}
}
