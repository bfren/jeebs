using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jm.Link.Unwrap;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public IR<TSingle> UnwrapSingle<TSingle>()
			=> result switch
			{
				IOkV<TValue> x => x.Value switch
				{
					IEnumerable<TSingle> y => y.Count() switch
					{
						1 => x.OkV(y.Single()),
						_ => x.Error<TSingle>().AddMsg().OfType<MoreThanOneItemMsg>()
					},
					IEnumerable _ => x.Error<TSingle>().AddMsg().OfType<IncorrectTypeMsg>(),
					TSingle y => x.OkV(y),
					_ => result.Error<TSingle>().AddMsg().OfType<NotIEnumerableMsg>()
				},
				_ => result.Error<TSingle>()
			};
	}
}
