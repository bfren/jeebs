// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc/>
		new public IR<TNext, TState> Wrap<TNext>(TNext value) =>
			result switch
			{
				IOk<TValue, TState> x =>
					Catch(() => x.OkV(value)),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc/>
		new public IR<TNext, TState> Wrap<TNext>(Func<TNext> f) =>
			result switch
			{
				IOk<TValue, TState> x =>
					Catch(() =>
					{
						var v = f();
						return x.OkV(v);
					}),

				_ =>
					result.Error<TNext>()
			};
	}
}
