using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link<TValue, TState>
	{
		new public IR<TValue, TState> Wrap(TValue value)
			=> result switch
			{
				IOk<TValue, TState> x => x.Catch(() => x.OkV(value)),
				_ => result.Error()
			};

		new public IR<TValue, TState> Wrap(Func<TValue> f)
			=> result switch
			{
				IOk<TValue, TState> x => x.Catch(() => { var v = f(); return x.OkV(v); }),
				_ => result.Error()
			};
	}
}
