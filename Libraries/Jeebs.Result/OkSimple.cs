using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a simple OK result without a value
	/// </summary>
	public class Ok : Ok<bool>, IOk
	{
		internal Ok() { }
	}
}
