using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOkV{TResult, TState}"/>
	public class OkV<TResult, TState> : Ok<TResult, TState>, IOkV<TResult, TState>
	{
		/// <summary>
		/// Success value.
		/// </summary>
		new public TResult Val { get; }

		internal OkV(TResult val, TState state) : base(state) => Val = val;
	}
}
