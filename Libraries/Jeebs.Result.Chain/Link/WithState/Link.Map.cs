// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private IR<TNext, TState> PrivateMap<TResult, TNext>(Func<TResult, IR<TNext, TState>> f)
			where TResult : IOk<TValue, TState> =>
			result switch
			{
				TResult x =>
					Catch(() => f(x)),

				_ =>
					result.Error<TNext>()
			};

		private IR<TNext, TState> PrivateMapAddState<TResult, TNext>(Func<TResult, IR<TNext>> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch<TNext>(() => f(x) switch
					{
						IOk<TNext> y =>
							y.WithState(result.State),

						_ =>
							result.Error<TNext>()
					}),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc cref="Link{TValue}.Map{TNext}(Func{IOk, IR{TNext}})"/>
		new public IR<TNext, TState> Map<TNext>(Func<IOk, IR<TNext>> f0) =>
			PrivateMapAddState(f0);

		/// <inheritdoc cref="Link{TValue}.Map{TNext}(Func{IOk{TValue}, IR{TNext}})"/>
		new public IR<TNext, TState> Map<TNext>(Func<IOk<TValue>, IR<TNext>> f1) =>
			PrivateMapAddState(f1);

		/// <inheritdoc cref="Link{TValue}.Map{TNext}(Func{IOkV{TValue}, IR{TNext}})"/>
		new public IR<TNext, TState> Map<TNext>(Func<IOkV<TValue>, IR<TNext>> f2) =>
			PrivateMapAddState(f2);

		/// <inheritdoc/>
		public IR<TNext, TState> Map<TNext>(Func<IOk<TValue, TState>, IR<TNext, TState>> f3) =>
			PrivateMap(f3);

		/// <inheritdoc/>
		public IR<TNext, TState> Map<TNext>(Func<IOkV<TValue, TState>, IR<TNext, TState>> f4) =>
			PrivateMap(f4);
	}
}
