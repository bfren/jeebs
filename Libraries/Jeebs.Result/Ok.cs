using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents a simple OK result without a value
	/// </summary>
	public class Ok : Ok<object>
	{
		internal Ok() { }
	}

	/// <summary>
	/// Represents an OK result without a value
	/// </summary>
	public class Ok<T> : R<T>
	{
		internal Ok() { }
	}

	/// <summary>
	/// Represents an OK result with a value
	/// </summary>
	/// <typeparam name="T">Result value type</typeparam>
	public class OkV<T> : Ok<T>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		public T Val { get; }

		internal OkV(T val) => Val = val;
	}
}
