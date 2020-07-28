using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		/// <inheritdoc/>
		public IR<TValue> Wrap(TValue value)
			=> result switch
			{
				IOk x => x.Catch(() => x.OkV(value)),
				_ => result.Error()
			};

		/// <inheritdoc/>
		public IR<TValue> Wrap(Func<TValue> f)
			=> result switch
			{
				IOk x => x.Catch(() => { var v = f(); return x.OkV(v); }),
				_ => result.Error()
			};
	}
}
