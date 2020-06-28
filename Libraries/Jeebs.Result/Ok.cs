using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Represents an OK result without a value
	/// </summary>
	public class Ok<T> : R<T>, IOk<T>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		public override bool Val => true;

		internal Ok() { }
	}
}
