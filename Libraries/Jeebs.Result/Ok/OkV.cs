using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOkV{TResult}"/>
	public class OkV<TResult> : Ok<TResult>, IOkV<TResult>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		new public TResult Val { get; }

		internal OkV(TResult val) => Val = val;
	}
}
