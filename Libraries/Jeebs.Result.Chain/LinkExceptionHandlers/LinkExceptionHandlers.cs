﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	internal sealed class LinkExceptionHandlers<TValue>
	{
		private readonly Dictionary<Type, Action<IR<TValue>, Exception>> handlers;

		public LinkExceptionHandlers()
			=> handlers = new Dictionary<Type, Action<IR<TValue>, Exception>>();

		internal void Add<TException>(Action<IR<TValue>, TException> handler)
			where TException : Exception
			=> handlers[typeof(TException)] = (r, ex) => { if (ex is TException t) handler(r, t); };

		internal Action<IR<TValue>, Exception>? Get(Type ex)
			=> handlers.TryGetValue(ex, out var value) ? value : null;
	}
}
