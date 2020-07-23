using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs
{
	/// <inheritdoc cref="IR{TValue, TState}"/>
	public abstract class R<TValue, TState> : R<TValue>, IR<TValue, TState>
	{
		/// <inheritdoc/>
		public TState State { get; }

		internal protected R(TState state) => State = state;

		/// <inheritdoc cref="IR.SkipAhead"/>
		new public IError<TValue, TState> SkipAhead() => SkipAhead<TValue>();

		/// <inheritdoc cref="IR.SkipAhead{TValue}"/>
		new public IError<TNext, TState> SkipAhead<TNext>() => this switch
		{
			IError<TNext, TState> e => e,
			_ => new RError<TNext, TState>(State) { Messages = Messages }
		};

		#region Explicit implementations

		/// <inheritdoc/>
		IError<TValue, TState> IR<TValue, TState>.SkipAhead() => SkipAhead();

		#endregion
	}
}
