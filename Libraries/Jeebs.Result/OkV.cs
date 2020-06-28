using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an OK result with a value
	/// </summary>
	/// <typeparam name="T">Result value type</typeparam>
	public class OkV<T> : Ok<T>, IOkV<T>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		new public T Val { get; }

		internal OkV(T val) => Val = val;
	}
}
