﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	public partial class Link
	{
		/// <inheritdoc/>
		public IR Run(Action a)
			=> result switch
			{
				IOk ok => ok.Catch(() => { a(); return ok; }),
				_ => result.Error()
			};

		/// <inheritdoc/>
		public IR Run(Action<IOk> a)
			=> result switch
			{
				IOk ok => ok.Catch(() => { a(ok); return ok; }),
				_ => result.Error()
			};
	}
}
