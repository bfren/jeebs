// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs
{
	/// <inheritdoc cref="IR{TValue}"/>
	public abstract class R<TValue> : IR<TValue>
	{
		/// <inheritdoc/>
		public IMsgList Messages { get; internal init; } = new MsgList();

		/// <inheritdoc/>
		public ILogger Logger { get; internal init; } = new Logger();

		internal R() { }

		/// <summary>
		/// Clear all messages and log
		/// </summary>
		public virtual void Dispose()
		{
			Messages.Dispose();
			Logger.Dispose();
		}

		/// <inheritdoc/>
		public IError<TValue> Error() =>
			Error<TValue>();

		/// <inheritdoc/>
		public IError<TNext> Error<TNext>() =>
			this switch
			{
				IError<TNext> e =>
					e,

				_ =>
					new RError<TNext> { Messages = Messages, Logger = Logger }
			};

		/// <inheritdoc/>
		public IR<TNext> Switch<TNext>(Func<IOkV<TValue>, IR<TNext>> okV, Func<IR<TValue>, IR<TNext>>? other = null) =>
			this switch
			{
				IOkV<TValue> x =>
					okV(x),

				_ =>
					other?.Invoke(this) ?? Error<TNext>()
			};

		#region Explicit implementations

		IError IR.Error() =>
			Error();

		#endregion
	}
}
