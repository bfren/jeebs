using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="IR{TValue}"/>
	public abstract class R<TValue> : IR<TValue>
	{
		/// <inheritdoc/>
		public MsgList Messages { get; internal set; } = new MsgList();

		/// <inheritdoc/>
		public Logger Logger { get; internal set; } = new Logger();

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
		public IError<TValue> Error()
			=> Error<TValue>();

		/// <inheritdoc/>
		public IError<TNext> Error<TNext>()
			=> this switch
			{
				IError<TNext> e => e,
				_ => new RError<TNext> { Messages = Messages, Logger = Logger }
			};

		/// <inheritdoc/>
		public IError<bool> False(IMsg? message = null)
		{
			if (message is IMsg msg)
			{
				Messages.Add(msg);
			}

			return Error<bool>();
		}

		#region Explicit implementations

		IError IR.Error()
			=> Error();

		#endregion
	}
}
