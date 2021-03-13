// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using static F.OptionF;

namespace Jeebs
{
	public abstract partial class Option<T>
	{
		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}}, Handler?)"/>
		internal Option<U> DoBind<U>(Func<T, Option<U>> bind, Handler? handler = null) =>
			F.OptionF.Bind(this, bind, handler);

		/// <inheritdoc cref="Bind{T, U}(Option{T}, Func{T, Option{U}}, Handler?)"/>
		public Option<U> Bind<U>(Func<T, Option<U>> bind, Handler? handler = null) =>
			F.OptionF.Bind(this, bind, handler);
	}
}
