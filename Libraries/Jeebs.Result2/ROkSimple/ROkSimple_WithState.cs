using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <summary>
	/// Simple result success with state - value type is <see cref="bool"/> - used to start a new chain in <see cref="R.Chain{TState}(TState)"/>
	/// </summary>
	/// <typeparam name="TState">Chain state type</typeparam>
	public sealed class ROkSimple<TState> : ROk<bool, TState>
	{
		internal ROkSimple(TState state) : base(state) { }
	}
}
