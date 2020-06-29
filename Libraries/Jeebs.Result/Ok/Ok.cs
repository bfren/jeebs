using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOk{TResult}"/>
	public class Ok<TResult> : R<TResult>, IOk<TResult>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		public override bool Val => true;

		internal Ok() { }
	}
}
