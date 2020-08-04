using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public IR<TSingle> Single<TSingle>()
			=> result switch
			{
				IOkV<TValue> x => x.Value switch
				{
					IEnumerable<TSingle> y when y.Count() == 1 => x.OkV(y.Single()),
					IEnumerable<TSingle> _ => x.Error<TSingle>().AddMsg().OfType<Jm.ExpectingSingleReceivingMoreMsg>(),
					IEnumerable _ => x.Error<TSingle>().AddMsg().OfType<Jm.IncorrectSingleTypeMsg>(),
					_ => result.Error<TSingle>().AddMsg().OfType<Jm.NotIEnumerableMsg>()
				},
				_ => result.Error<TSingle>()
			};
	}
}
