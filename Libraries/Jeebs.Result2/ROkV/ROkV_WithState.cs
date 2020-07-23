using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOkV{TValue, TState}"/>
	public class ROkV<TValue, TState> : ROk<TValue, TState>, IOkV<TValue, TState>
	{
		/// <inheritdoc/>
		public TValue Value { get; }

		internal ROkV(TValue value, TState state) : base(state) => Value = value;
	}
}
