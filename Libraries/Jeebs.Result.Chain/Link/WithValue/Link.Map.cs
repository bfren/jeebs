using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private IR<TNext> PrivateMap<TResult, TNext>(Func<TResult, IR<TNext>> f)
			where TResult : IOk<TValue>
			=> result switch
			{
				TResult x => x.Catch(() => f(x)),
				_ => result.Error<TNext>()
			};

		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOk<TValue>, IR<TNext>> f)
			=> PrivateMap(f);

		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOkV<TValue>, IR<TNext>> f)
			=> PrivateMap(f);
	}
}
