// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Match{T, U}(Option{T}, Func{T, U}, Func{IMsg?, U})"/>
		[Obsolete]
		internal U DoMatch<U>(Func<T, U> some, Func<IMsg?, U> none) =>
			F.OptionF.Match(this, some: some, none: none);
	}
}
