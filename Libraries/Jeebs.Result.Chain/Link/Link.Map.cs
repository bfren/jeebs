using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		/// <inheritdoc/>
		public IR<TNext> Map<TNext>(Func<IOk, IR<TNext>> f)
			=> result switch
			{
				IOk x => x.Catch(() => f(x)),
				_ => result.Error<TNext>()
			};
	}
}
