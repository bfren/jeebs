// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		internal Option<U> DoMap<U>(Func<T, U> map, Handler? handler = null) =>
			F.OptionF.Map(this, map, handler);

		/// <inheritdoc cref="Map{T, U}(Option{T}, Func{T, U}, Handler?)"/>
		public Option<U> Map<U>(Func<T, U> map, Handler? handler = null) =>
			F.OptionF.Map(this, map, handler);
	}
}
