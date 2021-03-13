// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.UnwrapSingle{T, U}(Option{T}, Func{IMsg}?, Func{IMsg}?, Func{IMsg}?)"/>
		[Obsolete]
		internal Option<U> DoUnwrapSingle<U>(Func<IMsg>? noItems, Func<IMsg>? tooMany, Func<IMsg>? notAList) =>
			F.OptionF.UnwrapSingle<T, U>(this, noItems, tooMany, notAList);
	}
}
