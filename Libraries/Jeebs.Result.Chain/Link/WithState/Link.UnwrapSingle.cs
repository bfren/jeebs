using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jm.Link.Unwrap;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		/// <inheritdoc cref="ILink{TValue, TState}.UnwrapSingle{TSingle}"/>
		new public IR<TSingle, TState> UnwrapSingle<TSingle>()
			=> result switch
			{
				IOkV<TValue, TState> x => x.Value switch
				{
					IEnumerable<TSingle> y when y.Count() == 1 => x.OkV(y.Single()),
					IEnumerable<TSingle> _ => x.Error<TSingle>().AddMsg().OfType<MoreThanOneItemMsg>(),
					IEnumerable _ => x.Error<TSingle>().AddMsg().OfType<IncorrectTypeMsg>(),
					TSingle y => x.OkV(y),
					_ => result.Error<TSingle>().AddMsg().OfType<NotIEnumerableMsg>()
				},
				_ => result.Error<TSingle>()
			};
	}
}
