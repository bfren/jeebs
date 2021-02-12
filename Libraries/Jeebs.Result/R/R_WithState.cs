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

		internal R(TState state) =>
			State = state;

		/// <inheritdoc cref="IR.Error"/>
		new public IError<TValue, TState> Error() =>
			Error<TValue>();

		/// <inheritdoc cref="IR.Error{TValue}"/>
		new public IError<TNext, TState> Error<TNext>() =>
			this switch
			{
				IError<TNext, TState> e =>
					e,

				_ =>
					new RError<TNext, TState>(State) { Messages = Messages, Logger = Logger }
			};

		/// <inheritdoc/>
		public IR<TNext, TState> Switch<TNext>(Func<IOkV<TValue>, IR<TNext, TState>> okV, Func<IR<TValue, TState>, IR<TNext, TState>>? other = null) =>
			this switch
			{
				IOkV<TValue, TState> x =>
					okV(x),

				_ =>
					other?.Invoke(this) ?? Error<TNext>()
			};

		#region Explicit implementations

		IError<TValue, TState> IR<TValue, TState>.Error() =>
			Error();

		#endregion
	}
}
