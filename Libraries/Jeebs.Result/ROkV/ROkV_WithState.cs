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

		internal ROkV(TValue value, TState state) : base(state)
			=> Value = value;

		#region Explicit implementations

		IOkV<TValue, TNext> IOkV<TValue>.WithState<TNext>(TNext state)
			=> new ROkV<TValue, TNext>(Value, state) { Messages = Messages };

		#endregion
	}
}
