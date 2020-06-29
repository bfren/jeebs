using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOk{TResult, TState}"/>
	public class Ok<TResult, TState> : R<TResult, TState>, IOk<TResult, TState>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		public override bool Val => true;

		internal Ok(TState state) : base(state) { }
	}
}
