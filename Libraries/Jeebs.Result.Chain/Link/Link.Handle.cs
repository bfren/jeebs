// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;
using Jeebs.Fluent;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public Handle<TValue, Exception> Handle() =>
			new(this);

		/// <inheritdoc/>
		public Handle<TValue, TException> Handle<TException>()
			where TException : Exception =>
			new(this);
	}
}
