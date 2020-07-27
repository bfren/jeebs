using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IOkV{TValue}"/>
	public class ROkV<TValue> : ROk<TValue>, IOkV<TValue>
	{
		/// <inheritdoc/>
		public TValue Value { get; }

		internal ROkV(TValue value) => Value = value;
	}
}
