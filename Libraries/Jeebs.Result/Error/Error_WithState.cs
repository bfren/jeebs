using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IError{TResult, TState}"/>
	public class Error<TResult, TState> : R<TResult, TState>, IError<TResult, TState>
	{
		/// <summary>
		/// Error value.
		/// </summary>
		public override bool Val => false;

		internal Error(TState state) : base(state) { }
	}
}
