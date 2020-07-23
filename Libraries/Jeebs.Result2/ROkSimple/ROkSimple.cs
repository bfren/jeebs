using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Simple result success - value type is <see cref="bool"/> - used to start a new chain in <see cref="R.Chain"/>
	/// </summary>
	public sealed class ROkSimple : ROk<bool>
	{
		internal ROkSimple() { }
	}
}
