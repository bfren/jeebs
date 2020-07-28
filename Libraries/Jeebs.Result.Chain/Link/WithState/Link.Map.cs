using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		private IR<TNext, TState> PrivateMap<TResult, TNext>(Func<TResult, IR<TNext, TState>> f)
			where TResult : IOk<TValue, TState>
			=> result switch
			{
				TResult x => x.Catch(() => f(x)),
				_ => result.Error<TNext>()
			};

		/// <inheritdoc/>
		public IR<TNext, TState> Map<TNext>(Func<IOk<TValue, TState>, IR<TNext, TState>> f)
			=> PrivateMap(f);

		/// <inheritdoc/>
		public IR<TNext, TState> Map<TNext>(Func<IOkV<TValue, TState>, IR<TNext, TState>> f)
			=> PrivateMap(f);
	}
}
