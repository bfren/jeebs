// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="FilterAsync{T}(Option{T}, Func{T, Task{bool}}, Handler?)"/>
		[Obsolete]
		internal Task<Option<T>> DoFilterAsync(Func<T, Task<bool>> predicate, Handler? handler) =>
			F.OptionF.FilterAsync(this, predicate, handler);
	}
}
