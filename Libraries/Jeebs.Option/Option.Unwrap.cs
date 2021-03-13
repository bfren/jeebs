// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="F.OptionF.Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		internal T DoUnwrap(Func<IMsg?, T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone);

		/// <inheritdoc cref="F.OptionF.Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(T ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone);

		/// <inheritdoc cref="F.OptionF.Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(Func<T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: _ => ifNone());

		/// <inheritdoc cref="F.OptionF.Unwrap{T}(Option{T}, Func{IMsg?, T})"/>
		public T Unwrap(Func<IMsg?, T> ifNone) =>
			F.OptionF.Unwrap(this, ifNone: ifNone);
	}
}
