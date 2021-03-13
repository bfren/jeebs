// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		[Obsolete]
		internal T DoUnwrap(Func<IMsg?, T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone);
	}
}
