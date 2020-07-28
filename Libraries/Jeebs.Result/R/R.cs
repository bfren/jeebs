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
		public MsgList Messages { get; internal set; }

		internal R()
			=> Messages = new MsgList();

		/// <summary>
		/// Clear all messages
		/// </summary>
		public virtual void Dispose()
			=> Messages.Clear();

		/// <inheritdoc/>
		public IError<TValue> Error()
			=> Error<TValue>();

		/// <inheritdoc/>
		public IError<TNext> Error<TNext>()
			=> this switch
			{
				IError<TNext> e => e,
				_ => new RError<TNext> { Messages = Messages }
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
