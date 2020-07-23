using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IError{TValue, TState}"/>
	public class RError<TValue, TState> : R<TValue, TState>, IError<TValue, TState>
	{
		internal RError(TState state) : base(state) { }
	}
}
