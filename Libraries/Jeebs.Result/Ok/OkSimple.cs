using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOk"/>
	public class Ok : Ok<bool>, IOk
	{
		internal Ok() { }
	}
}
