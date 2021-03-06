// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public IR<TNext> Wrap<TNext>(TNext value) =>
			result switch
			{
				IOk x =>
					x.OkV(value),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc/>
		public IR<TNext> Wrap<TNext>(Func<TNext> f) =>
			result switch
			{
				IOk x =>
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
