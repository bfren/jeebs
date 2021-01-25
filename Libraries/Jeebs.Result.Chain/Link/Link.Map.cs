using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private IR<TNext> PrivateMap<TResult, TNext>(Func<TResult, IR<TNext>> f)
			where TResult : IOk =>
			result switch
			{
				TResult x =>
					Catch(() => f(x)),

				_ =>
					result.Error<TNext>()
			};

		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOk, IR<TNext>> f0) =>
			PrivateMap(f0);

		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOk<TValue>, IR<TNext>> f1) =>
			PrivateMap(f1);

		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOkV<TValue>, IR<TNext>> f2) =>
			PrivateMap(f2);
	}
}
