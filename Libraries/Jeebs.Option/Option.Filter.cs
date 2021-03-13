// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <summary>
		/// Return the current type if it is <see cref="Some{T}"/> and the predicate is true
		/// </summary>
		/// <param name="predicate">Predicate - if this is a <see cref="Some{T}"/> it receives the Value</param>
		/// <param name="handler">[Optional] Exception handler</param>
		internal Option<T> DoFilter(Func<T, bool> predicate, Handler? handler) =>
			F.OptionF.Filter(this, predicate, handler);

		/// <inheritdoc cref="DoFilter(Func{T, bool}, Handler?)"/>
		public Option<T> Filter(Func<T, bool> predicate, Handler? handler = null) =>
			F.OptionF.Filter(this, predicate, handler);
	}
}
