using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Result success with a value, used when result has a state
	/// </summary>
	/// <typeparam name="TValue">Result value type</typeparam>
	/// <typeparam name="TState">Result state type</typeparam>
	public interface IOkV<TValue, TState> : IOkV<TValue>, IOk<TValue, TState>
	{

	}
}
