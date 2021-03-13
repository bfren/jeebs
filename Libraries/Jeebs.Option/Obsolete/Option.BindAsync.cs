// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}}, Handler?)"/>
		[Obsolete]
		internal Task<Option<U>> DoBindAsync<U>(Func<T, Task<Option<U>>> bind, Handler? handler) =>
			F.OptionF.BindAsync(this, bind, handler);
	}
}
