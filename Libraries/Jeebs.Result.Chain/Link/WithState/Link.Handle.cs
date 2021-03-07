// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Fluent;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public Handle<TValue, TState, Exception> Handle() =>
			new(this);

		/// <inheritdoc/>
		new public Handle<TValue, TState, TException> Handle<TException>()
			where TException : Exception =>
			new(this);
	}
}
