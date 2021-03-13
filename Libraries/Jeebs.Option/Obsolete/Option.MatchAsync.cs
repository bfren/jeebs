// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		[Obsolete]
		internal Task<U> DoMatchAsync<U>(Func<T, Task<U>> some, Func<IMsg?, Task<U>> none) =>
			F.OptionF.MatchAsync(this, some: v => some(v), none: r => none(r));
	}
}
