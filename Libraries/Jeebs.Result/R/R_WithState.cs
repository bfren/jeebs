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

		internal R(TState state)
			=> State = state;

		/// <inheritdoc cref="IR.Error"/>
		new public IError<TValue, TState> Error()
			=> Error<TValue>();

		/// <inheritdoc cref="IR.LogUsing(ILog)"/>
		public new IR<TValue, TState> LogUsing(ILog log)
		{
			base.LogUsing(log);
			return this;
		}

		/// <inheritdoc cref="IR.Error{TValue}"/>
		new public IError<TNext, TState> Error<TNext>()
			=> this switch
			{
				IError<TNext, TState> e => e,
				_ => new RError<TNext, TState>(State) { Messages = Messages, Log = Log }
			};

		/// <inheritdoc cref="IR.Error{TNext}"/>
		new public IError<bool, TState> False(IMsg? message = null)
		{
			if (message is IMsg msg)
			{
				Messages.Add(msg);
			}

			return Error<bool>();
		}

		#region Explicit implementations

		IError<TValue, TState> IR<TValue, TState>.Error()
			=> Error();

		IError<bool> IR.False(IMsg? message)
			=> False(message);

		#endregion
	}
}
