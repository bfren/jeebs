using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	public partial class Link<TValue>
	{
		private IR<TValue> PrivateRun<TResult>(Action<TResult> f)
			where TResult : IOk
			=> result switch
			{
				TResult x => x.Catch(() => { f(x); return result; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		new public IR<TValue> Run(Action f)
			=> result switch
			{
				IOk<TValue> x => x.Catch(() => { f(); return x; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		new public IR<TValue> Run(Action<IOk> f)
			=> PrivateRun(f);

		/// <inheritdoc/>
		public IR<TValue> Run(Action<IOk<TValue>> f)
			=> PrivateRun(f);

		/// <inheritdoc/>
		public IR<TValue> Run(Action<IOkV<TValue>> f)
			=> PrivateRun(f);
	}
}
