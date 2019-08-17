using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.WordPress
{
	public sealed class Wp
	{
		public WpConfig Config { get; }

		public Wp(WpConfig config)
		{
			Config = config;
		}
	}
}
