using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		public IR<TValue> Wrap<TValue>(TValue value)
			=> result switch
			{
				IOk x => x.Catch(() => x.OkV(value)),
				_ => result.Error<TValue>()
			};

		public IR<TValue> Wrap<TValue>(Func<TValue> f)
			=> result switch
			{
				IOk x => x.Catch(() => { var v = f(); return x.OkV(v); }),
				_ => result.Error<TValue>()
			};
	}
}
